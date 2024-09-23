using FluentValidation;
using Microsoft.Extensions.Logging;
using PetHome.Application.Validation;
using PetHome.Domain.Shared;
using PetHome.Domain.Shared.IDs;

namespace PetHome.Application.VolunteersManagement.Restore
{
    public class RestoreVolunteerHandler
    {
        private readonly IVolunteerRepository _volunteerRepository;
        private readonly ILogger<RestoreVolunteerHandler> _logger;
        private readonly IValidator<RestoreVolunteerCommand> _validator;

        public RestoreVolunteerHandler(
            IVolunteerRepository volunteerRepository,
            ILogger<RestoreVolunteerHandler> logger,
            IValidator<RestoreVolunteerCommand> validator)
        {
            _volunteerRepository = volunteerRepository;
            _logger = logger;
            _validator = validator;
        }

        public async Task<Result<Guid>> Execute(
            RestoreVolunteerCommand command,
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
                return volunteerResult.Error;

            var result = await _volunteerRepository.Restore(volunteerResult.Value, token);

            _logger.LogInformation("Restore volunteer with id {volunteerId}", volunteerResult);

            return volunteerResult.Value.Id.Id;
        }
    }
}
