using Microsoft.Extensions.Logging;
using PetHome.Domain.PetManadgement.ValueObjects;
using PetHome.Domain.Shared;
using PetHome.Domain.Shared.IDs;

namespace PetHome.Application.Volunteers.UpdateSocialNetworks
{
    public class UpdateSocialNetworksHandler
    {
        private readonly IVolunteerRepository _volunteerRepository;
        private readonly ILogger<UpdateSocialNetworksHandler> _logger;

        public UpdateSocialNetworksHandler(IVolunteerRepository volunteerRepository,
            ILogger<UpdateSocialNetworksHandler> logger)
        {
            _volunteerRepository = volunteerRepository;
            _logger = logger;
        }

        public async Task<Result<Guid>> Execute(
            UpdateSocialNetworksCommand request,
            CancellationToken token)
        {
            var volunteerId = VolunteerId.Create(request.VolunteerId);
            var volunteerResult =
                await _volunteerRepository.GetById(volunteerId, token);

            if (volunteerResult.IsFailure)
                return volunteerResult.Error;

            var socialColl = (from item in request.SocialNetworks
                              let socialNetwork = SocialNetwork
                                    .Create(item.Name, item.Path).Value
                              select socialNetwork).ToList();

            var socialNetworkCollection = new SocialNetworks(socialColl);

            volunteerResult.Value.UpdateSocialNetworks(
               socialNetworkCollection);

            var result =
                await _volunteerRepository.Update(volunteerResult.Value, token);

            _logger.LogInformation(
                "Updated social networks with id {volunteerId}",
                result);

            return result;
        }
    }
}
