using FluentValidation;
using Microsoft.Extensions.Logging;
using PetHome.Application.Abstractions;
using PetHome.Application.Dtos;
using PetHome.Application.Extensions;
using PetHome.Application.FileProvider;
using PetHome.Application.Messaging;
using PetHome.Application.VolunteersManagement.Commands.PetManagement.AddPetFiles;
using PetHome.Domain.PetManadgement.Entities;
using PetHome.Domain.Shared;
using PetHome.Domain.Shared.IDs;
using FileInfo = PetHome.Application.FileProvider.FileInfo;
using IFileProvider = PetHome.Application.FileProvider.IFileProvider;

namespace PetHome.Application.VolunteersManagement.Commands.PetManagement.UpdateFiles
{
    public class UpdateFilesHandler : ICommandHandler<int, UpdateFilesCommand>
    {
        private readonly IVolunteerRepository _volunteerRepository;
        private readonly IFileProvider _fileProvider;
        private readonly IMessageQueue<FileInfo> _messageQueue;
        private readonly ILogger<UpdateFilesHandler> _logger;
        private readonly IValidator<UpdateFilesCommand> _validator;

        public UpdateFilesHandler(
            IVolunteerRepository volunteerRepository,
            IFileProvider fileProvider,
            IMessageQueue<FileInfo> messageQueue,
            ILogger<UpdateFilesHandler> logger,
            IValidator<UpdateFilesCommand> validator)
        {
            _volunteerRepository = volunteerRepository;
            _fileProvider = fileProvider;
            _messageQueue = messageQueue;
            _logger = logger;
            _validator = validator;
        }

        public async Task<Result<int>> Execute(UpdateFilesCommand command, CancellationToken token)
        {
            var validationResult = await _validator.ValidateAsync(command, token);
            if (validationResult.IsValid == false)
            {
                return validationResult.ToErrorList();
            }

            var volunteerResult = await _volunteerRepository
                .GetById(VolunteerId.Create(command.VolunteerId), token);
            if (volunteerResult.IsFailure)
            {
                return volunteerResult.Error;
            }

            var petResult = volunteerResult.Value.Pets
                .FirstOrDefault(p => p.Id.Id == command.petId);
            if (petResult == null)
            {
                return Errors.General.NotFound(command.petId);
            }

            var petPhotosResult = await GetPhotos(command, token);
            if (petPhotosResult.IsFailure)
            {
                return petPhotosResult.Error;
            }

            petResult.DeletePhotos();
            
            var result = petResult.SetPhotos(petPhotosResult.Value);
            if (result.IsFailure)
            {
                return result.Error;
            }

            await _volunteerRepository.Update(volunteerResult.Value, token);

            _logger.LogInformation("Add pet photos, Id: {petId}", petResult.Id);

            return result;
        }

        private async Task<Result<List<PetPhoto>>> GetPhotos(
            UpdateFilesCommand command,
            CancellationToken token)
        {
            List<PetPhoto> petPhotos = [];
            var semaphore = new SemaphoreSlim(AddPetFilesHandler.MAX_SEMAPHORE_TASKS);
            var fileInfoCollection = new List<FileInfo>();

            var existFilesResult = await _fileProvider
                .GetFiles(new FileInfo(AddPetFilesHandler.BUCKET_NAME, string.Empty), token);            

            foreach (var file in command.FilesList)
            {
                var filePath = GetFilePath(file);
                if (filePath.IsFailure)
                {
                    return filePath.Error;
                }

                var fileInfo = new FileInfo(AddPetFilesHandler.BUCKET_NAME, filePath.Value.Path);

                fileInfoCollection.Add(fileInfo);

                var fileData = new FileData(file.Stream, fileInfo);

                var upLoadResult = await UploadFile(
                    fileData,
                    fileInfoCollection,
                    semaphore,
                    token);

                if (upLoadResult.IsFailure)
                {
                    return upLoadResult.Error;
                }

                var photo = GetPhoto(filePath.Value);
                if (photo.IsFailure)
                {
                    return photo.Error;
                }

                petPhotos.Add(photo.Value);
            }

            if (existFilesResult.IsSuccess)
            {
                var deleteList = new List<FileInfo>();

                foreach (var fileName in existFilesResult.Value)
                {
                    deleteList.Add(new FileInfo(AddPetFilesHandler.BUCKET_NAME, fileName));
                }

                if(deleteList.Count > 0)
                {
                    await _messageQueue.WriteAsync(deleteList, token);
                }                
            }

            return petPhotos;
        }

        private async Task<Result> UploadFile(
            FileData fileData,
            List<FileInfo> fileInfoCollection,
            SemaphoreSlim semaphore,
            CancellationToken token)
        {
            try
            {
                await semaphore.WaitAsync(token);

                var uploadResult = await _fileProvider.UploadFile(fileData, token);
                if (uploadResult.IsFailure)
                {
                    await _messageQueue.WriteAsync(fileInfoCollection, token);
                    return uploadResult.Error;
                }
            }
            finally
            {
                semaphore.Release();
            }

            return Result.Success();
        }

        private Result<FilePath> GetFilePath(FileDto file)
        {
            var extension = Path.GetExtension(file.FileName);
            return FilePath.Create(Guid.NewGuid(), extension);
        }

        private Result<PetPhoto> GetPhoto(FilePath filePath)
        {
            var photoId = PetPhotoId.NewPhotoId();
            return PetPhoto.Create(photoId, filePath, true);
        }
    }
}
