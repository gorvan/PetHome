using FluentValidation;
using Microsoft.Extensions.Logging;
using PetHome.Shared.Core.Abstractions;
using PetHome.Shared.Core.Extensions;
using PetHome.Shared.Core.Shared;
using PetHome.Shared.Core.Shared.IDs;

namespace PetHome.Volunteers.Application.VolunteersManagement.Commands.PetManagement.UpdatePetHelpStatus
{
    public class UpdatePetHelpStatusHandler : ICommandHandler<Guid, UpdatePetHelpStatusCommand>
    {
        private readonly IVolunteerRepository _volunteerRepository;
        private readonly IReadDbContextVolunteers _readDbContext;
        private readonly ILogger<UpdatePetHelpStatusHandler> _logger;
        private readonly IValidator<UpdatePetHelpStatusCommand> _validator;

        public UpdatePetHelpStatusHandler(
            IVolunteerRepository volunteerRepository,
            IReadDbContextVolunteers readDbContext,
            ILogger<UpdatePetHelpStatusHandler> logger,
            IValidator<UpdatePetHelpStatusCommand> validator)
        {
            _volunteerRepository = volunteerRepository;
            _readDbContext = readDbContext;
            _logger = logger;
            _validator = validator;
        }

        public async Task<Result<Guid>> Execute(
            UpdatePetHelpStatusCommand command,
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

            pet.UpdateHelpStatus(command.HelpStatus);

            var result =
                await _volunteerRepository.Update(volunteerResult.Value, token);

            _logger.LogInformation("Updated HelpStatus pet with id {pet.Id.Id}", pet.Id.Id);

            return result;
        }
    }
}
