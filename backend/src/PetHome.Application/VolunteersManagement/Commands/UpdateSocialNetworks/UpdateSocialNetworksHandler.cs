using FluentValidation;
using Microsoft.Extensions.Logging;
using PetHome.Application.Abstractions;
using PetHome.Application.Extensions;
using PetHome.Domain.PetManadgement.ValueObjects;
using PetHome.Domain.Shared;
using PetHome.Domain.Shared.IDs;

namespace PetHome.Application.VolunteersManagement.Commands.UpdateSocialNetworks
{
    public class UpdateSocialNetworksHandler 
        : ICommandHandler<Guid, UpdateSocialNetworksCommand>
    {
        private readonly IVolunteerRepository _volunteerRepository;
        private readonly ILogger<UpdateSocialNetworksHandler> _logger;
        private readonly IValidator<UpdateSocialNetworksCommand> _validator;

        public UpdateSocialNetworksHandler(
            IVolunteerRepository volunteerRepository,
            ILogger<UpdateSocialNetworksHandler> logger,
            IValidator<UpdateSocialNetworksCommand> validator)
        {
            _volunteerRepository = volunteerRepository;
            _logger = logger;
            _validator = validator;
        }

        public async Task<Result<Guid>> Execute(
            UpdateSocialNetworksCommand command,
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

            var socialColl = (from item in command.SocialNetworks
                              let socialNetwork = SocialNetwork
                                    .Create(item.Name, item.Path).Value
                              select socialNetwork).ToList();

            volunteerResult.Value.UpdateSocialNetworks(socialColl);

            var result =
                await _volunteerRepository.Update(volunteerResult.Value, token);

            _logger.LogInformation(
                "Updated social networks with id {volunteerId}",
                result);

            return result;
        }
    }
}
