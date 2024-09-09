﻿using Microsoft.Extensions.Logging;
using PetHome.Application.FileProvider;
using PetHome.Application.Providers;
using PetHome.Domain.PetManadgement.Entities;
using PetHome.Domain.Shared;
using PetHome.Domain.Shared.IDs;

namespace PetHome.Application.Volunteers.AddPetFiles
{
    public class AddPetFilesHandler
    {
        private readonly IVolunteerRepository _volunteerRepository;
        private readonly IFileProvider _fileProvider;
        private readonly ILogger<AddPetFilesHandler> _logger;

        public const string BUCKET_NAME = "photos";
        public const int MAX_SEMAPHORE_TASKS = 5;

        public AddPetFilesHandler(
            IVolunteerRepository volunteerRepository,
            IFileProvider fileProvider,
            ILogger<AddPetFilesHandler> logger)
        {
            _volunteerRepository = volunteerRepository;
            _fileProvider = fileProvider;
            _logger = logger;
        }

        public async Task<Result<int>> Execute(
            AddPetFilesCommand command,
            CancellationToken token)
        {
            var volunteerResult = await _volunteerRepository
                .GetById(VolunteerId.Create(command.VolunteerId), token);
            if (volunteerResult.IsFailure)            
                return volunteerResult.Error;
            
            var petResult = volunteerResult.Value.Pets
                .FirstOrDefault(p=>p.Id.Id == command.petId);
            if (petResult == null)
                return Errors.General.NotFound(command.petId);

            var petPhotosResult = await CreatePhotos(command, token);
            if (petPhotosResult.IsFailure)
                return petPhotosResult.Error;

            var result = petResult.SetPhotos(petPhotosResult.Value);
            if(result.IsFailure)
                return result.Error;

            await _volunteerRepository.Update(volunteerResult.Value, token);

            return result;
        }

        private async Task<Result<List<PetPhoto>>> CreatePhotos(
            AddPetFilesCommand command,
            CancellationToken token)
        {
            List<PetPhoto> petPhotos = [];
            var semaphore = new SemaphoreSlim(MAX_SEMAPHORE_TASKS);
            foreach (var file in command.FilesList)
            {
                var extension = Path.GetExtension(file.FileName);

                var filePath = FilePath.Create(Guid.NewGuid(), extension);

                if (filePath.IsFailure)
                    return filePath.Error;

                var filedata = new FileData(
                    file.Stream,
                    BUCKET_NAME,
                    filePath.Value.Path);

                try
                {
                    await semaphore.WaitAsync(token);
                    var uploadResult =
                    await _fileProvider.UploadFile(filedata, token);

                    if (uploadResult.IsFailure)
                        return uploadResult.Error;
                }
                finally
                {
                    semaphore.Release();
                }

                var photoId = PetPhotoId.NewPhotoId();

                var photo = PetPhoto.Create(photoId, filePath.Value, true);

                if (photo.IsFailure)
                    return photo.Error;

                petPhotos.Add(photo.Value);
            }
            return petPhotos;
        }
    }
}