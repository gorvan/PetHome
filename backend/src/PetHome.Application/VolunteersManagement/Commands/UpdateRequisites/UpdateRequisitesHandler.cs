using FluentValidation;
using Microsoft.Extensions.Logging;
using PetHome.Application.Abstractions;
using PetHome.Application.Extensions;
using PetHome.Domain.PetManadgement.ValueObjects;
using PetHome.Domain.Shared;
using PetHome.Domain.Shared.IDs;

namespace PetHome.Application.VolunteersManagement.Commands.UpdateRequisites
{
    public class UpdateRequisitesHandler : ICommandHandler<Guid, UpdateRequisitesCommand>
    {
        private readonly IVolunteerRepository _volunteerRepository;
        private readonly ILogger<UpdateRequisitesHandler> _logger;
        private readonly IValidator<UpdateRequisitesCommand> _validator;

        public UpdateRequisitesHandler(
            IVolunteerRepository volunteerRepository,
            ILogger<UpdateRequisitesHandler> logger,
            IValidator<UpdateRequisitesCommand> validator)
        {
            _volunteerRepository = volunteerRepository;
            _logger = logger;
            _validator = validator;
        }

        public async Task<Result<Guid>> Execute(
            UpdateRequisitesCommand command,
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

            var requisiteColl = (from item in command.Requisites
                                 let requisite = Requisite
                                    .Create(item.Name, item.Description).Value
                                 select requisite).ToList();

            var requisiteCollection = new VolunteersRequisites(requisiteColl);

            volunteerResult.Value.UpdateRequisites(
               requisiteCollection);

            var result =
                await _volunteerRepository.Update(volunteerResult.Value, token);

            _logger.LogInformation(
                "Updated requisites with id {volunteerId}",
                result);

            return result;
        }
    }
}
