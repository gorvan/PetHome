using PetHome.Application.Volunteers.Shared;
using PetHome.Application.VolunteersManagement.UpdateSocialNetworks;

namespace PetHome.API.Contracts
{
    public record UpdateSocialNetworksRequest(
        List<SocialNetworkDto> socialNetworkDtos)
    {
        public UpdateSocialNetworksCommand ToCommand(Guid id) =>
            new UpdateSocialNetworksCommand(id, socialNetworkDtos);
    }
}
