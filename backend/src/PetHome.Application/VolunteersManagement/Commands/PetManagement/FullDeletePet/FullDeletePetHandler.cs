﻿using FluentValidation;
using Microsoft.Extensions.Logging;
using PetHome.Application.Abstractions;
using PetHome.Application.Database;
using PetHome.Application.Extensions;
using PetHome.Application.FileProvider;
using PetHome.Application.VolunteersManagement.Commands.PetManagement.AddPetFiles;
using PetHome.Domain.Shared;
using PetHome.Domain.Shared.IDs;
using FileInfo = PetHome.Application.FileProvider.FileInfo;

namespace PetHome.Application.VolunteersManagement.Commands.PetManagement.FullDeletePet
{
    public class FullDeletePetHandler : ICommandHandler<Guid, FullDeletePetCommand>
    {
        private readonly IVolunteerRepository _volunteerRepository;
        private readonly IReadDbContext _readDbContext;
        private readonly IFileProvider _fileProvider;
        private readonly ILogger<FullDeletePetHandler> _logger;
        private readonly IValidator<FullDeletePetCommand> _validator;

        public FullDeletePetHandler(
            IVolunteerRepository volunteerRepository,
            IReadDbContext readDbContext,
            IFileProvider fileProvider,
            ILogger<FullDeletePetHandler> logger,
            IValidator<FullDeletePetCommand> validator)
        {
            _volunteerRepository = volunteerRepository;
            _readDbContext = readDbContext;
            _fileProvider = fileProvider;
            _logger = logger;
            _validator = validator;
        }

        public async Task<Result<Guid>> Execute(
            FullDeletePetCommand command,
            CancellationToken token)
        {
            var validationResult = await _validator.ValidateAsync(command, token);
            if (validationResult.IsValid == false)
            {
                return validationResult.ToErrorList();
            }

            var volunteerId = VolunteerId.Create(command.VolunteerId);
            var volunteerResult =
                await _volunteerRepository.GetById(volunteerId, token);

            if (volunteerResult.IsFailure)
            {
                return volunteerResult.Error;
            }

            var pet = volunteerResult.Value.Pets
                .FirstOrDefault(p => p.Id.Id == command.PetId);

            if (pet == null)
            {
                return Errors.General.NotFound(command.PetId);
            }

            var deleteResult = volunteerResult.Value.FullDeletePet(pet);

            if (deleteResult.IsFailure)
            {
                return deleteResult.Error;
            }

            var result = await _volunteerRepository.Update(volunteerResult.Value, token);

            foreach (var photo in pet.Photos)
            {
                await _fileProvider.RemoveFile(
                    new FileInfo(AddPetFilesHandler.BUCKET_NAME, photo.Path.Path)
                    , token);
            }

            _logger.LogInformation("Full delete pet with id {command.PetId}", command.PetId);

            return pet.Id.Id;
        }
    }
}
