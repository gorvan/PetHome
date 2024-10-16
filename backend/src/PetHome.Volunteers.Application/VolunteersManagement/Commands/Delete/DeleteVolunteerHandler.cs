using FluentValidation;
using Microsoft.Extensions.Logging;
using PetHome.Shared.Core.Abstractions;
using PetHome.Shared.Core.Extensions;
using PetHome.Shared.Core.Shared;
using PetHome.Shared.Core.Shared.IDs;

namespace PetHome.Volunteers.Application.VolunteersManagement.Commands.Delete
{
    public class DeleteVolunteerHandler : ICommandHandler<Guid, DeleteVolunteerCommand>
    {
        private readonly IVolunteerRepository _volunteerRepository;
        private readonly ILogger<DeleteVolunteerHandler> _logger;
        private readonly IValidator<DeleteVolunteerCommand> _validator;

        public DeleteVolunteerHandler(
            IVolunteerRepository volunteerRepository,
            ILogger<DeleteVolunteerHandler> logger,
            IValidator<DeleteVolunteerCommand> validator)
        {
            _volunteerRepository = volunteerRepository;
            _logger = logger;
            _validator = validator;
        }

        public async Task<Result<Guid>> Execute(
            DeleteVolunteerCommand command,
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

            var result = await _volunteerRepository.Delete(volunteerResult.Value, token);

            _logger.LogInformation("Delete volunteer with id {volunteerId}", volunteerResult);

            return volunteerResult.Value.Id.Id;
        }
    }
}
