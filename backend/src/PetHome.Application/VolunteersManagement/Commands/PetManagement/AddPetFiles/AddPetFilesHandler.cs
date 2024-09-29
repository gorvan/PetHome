using FluentValidation;
using Microsoft.Extensions.Logging;
using PetHome.Application.Abstractions;
using PetHome.Application.Dtos;
using PetHome.Application.Extensions;
using PetHome.Application.FileProvider;
using PetHome.Application.Messaging;
using PetHome.Domain.PetManadgement.Entities;
using PetHome.Domain.Shared;
using PetHome.Domain.Shared.IDs;
using FileInfo = PetHome.Application.FileProvider.FileInfo;

namespace PetHome.Application.VolunteersManagement.Commands.PetManagement.AddPetFiles
{
    public class AddPetFilesHandler : ICommandHandler<int, AddPetFilesCommand>
    {
        private readonly IVolunteerRepository _volunteerRepository;
        private readonly IFileProvider _fileProvider;
        private readonly IMessageQueue<FileInfo> _messageQueue;
        private readonly ILogger<AddPetFilesHandler> _logger;
        private readonly IValidator<AddPetFilesCommand> _validator;

        public const string BUCKET_NAME = "photos";
        public const int MAX_SEMAPHORE_TASKS = 5;

        public AddPetFilesHandler(
            IVolunteerRepository volunteerRepository,
            IFileProvider fileProvider,
            IMessageQueue<FileInfo> messageQueue,
            ILogger<AddPetFilesHandler> logger,
            IValidator<AddPetFilesCommand> validator)
        {
            _volunteerRepository = volunteerRepository;
            _fileProvider = fileProvider;
            _messageQueue = messageQueue;
            _logger = logger;
            _validator = validator;
        }

        public async Task<Result<int>> Execute(
            AddPetFilesCommand command,
            CancellationToken token)
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

            var petPhotosResult = await CreatePhotos(command, token);
            if (petPhotosResult.IsFailure)
            {
                return petPhotosResult.Error;
            }

            var result = petResult.SetPhotos(petPhotosResult.Value);
            if (result.IsFailure)
            {
                return result.Error;
            }

            await _volunteerRepository.Update(volunteerResult.Value, token);

            _logger.LogInformation("Add pet photos, Id: {petId}", petResult.Id);

            return result;
        }

        private async Task<Result<List<PetPhoto>>> CreatePhotos(
            AddPetFilesCommand command,
            CancellationToken token)
        {
            List<PetPhoto> petPhotos = [];
            var semaphore = new SemaphoreSlim(MAX_SEMAPHORE_TASKS);
            var fileInfoCollection = new List<FileInfo>();

            foreach (var file in command.FilesList)
            {
                var filePath = GetFilePath(file);
                if (filePath.IsFailure)
                {
                    return filePath.Error;
                }

                var fileInfo = new FileInfo(filePath.Value.Path, BUCKET_NAME);
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
