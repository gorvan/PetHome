﻿using FluentValidation;
using Microsoft.Extensions.Logging;
using PetHome.Shared.Core.Abstractions;
using PetHome.Shared.Core.Extensions;
using PetHome.Shared.Core.Shared;
using PetHome.Shared.Core.Shared.IDs;
using PetHome.Volunteers.Application;

namespace PetHome.Volunteers.Application.VolunteersManagement.Commands.PetManagement.DeletePet
{
    public class DeletePetHandler : ICommandHandler<Guid, DeletePetCommand>
    {
        private readonly IVolunteerRepository _volunteerRepository;
        private readonly IReadDbContextVolunteers _readDbContext;
        private readonly ILogger<DeletePetHandler> _logger;
        private readonly IValidator<DeletePetCommand> _validator;

        public DeletePetHandler(
            IVolunteerRepository volunteerRepository,
            IReadDbContextVolunteers readDbContext,
            ILogger<DeletePetHandler> logger,
            IValidator<DeletePetCommand> validator)
        {
            _volunteerRepository = volunteerRepository;
            _readDbContext = readDbContext;
            _logger = logger;
            _validator = validator;
        }

        public async Task<Result<Guid>> Execute(
            DeletePetCommand command,
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

            var deleteResult = volunteerResult.Value.SoftDeletePet(pet);

            if (deleteResult.IsFailure)
            {
                return deleteResult.Error;
            }

            var result =
                await _volunteerRepository.Update(volunteerResult.Value, token);

            _logger.LogInformation("Delete pet with id {volunteerId}", command.PetId);

            return pet.Id.Id;
        }
    }
}
