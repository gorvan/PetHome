﻿using FluentValidation;
using Microsoft.Extensions.Logging;
using PetHome.Application.Abstractions;
using PetHome.Application.Extensions;
using PetHome.Application.FileProvider;
using PetHome.Domain.Shared;
using PetHome.Domain.Shared.IDs;

namespace PetHome.Application.VolunteersManagement.Commands.PetManagement.SetMainPetPhoto
{
    public class SetMainPetPhotoHandler : ICommandHandler<SetMainPetPhotoCommand>
    {
        private readonly IVolunteerRepository _volunteerRepository;
        private readonly IFileProvider _fileProvider;
        private readonly ILogger<SetMainPetPhotoHandler> _logger;
        private readonly IValidator<SetMainPetPhotoCommand> _validator;

        public SetMainPetPhotoHandler(
            IVolunteerRepository volunteerRepository,
            IFileProvider fileProvider,
            ILogger<SetMainPetPhotoHandler> logger,
            IValidator<SetMainPetPhotoCommand> validator)
        {
            _volunteerRepository = volunteerRepository;
            _fileProvider = fileProvider;
            _logger = logger;
            _validator = validator;
        }

        public async Task<Result> Execute(SetMainPetPhotoCommand command, CancellationToken token)
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
                .FirstOrDefault(p => p.Id.Id == command.PetId);
            if (petResult == null)
            {
                return Errors.General.NotFound(command.PetId);
            }

            var result = petResult.SetMainPhoto(command.FilePath);

            if (result.IsFailure)
            {
                return result.Error;
            }

            await _volunteerRepository.Update(volunteerResult.Value, token);

            _logger.LogInformation("Pet photo: {command.FilePath} set main", command.FilePath);

            return Result.Success();
        }
    }
}
