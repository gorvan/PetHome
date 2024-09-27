using PetHome.Application.Dtos;
using PetHome.Application.VolunteersManagement.Commands.UpdateSocialNetworks;

namespace PetHome.API.Contracts
{
    public record UpdateSocialNetworksRequest(
        IEnumerable<SocialNetworkDto> socialNetworkDtos)
    {
        public UpdateSocialNetworksCommand ToCommand(Guid id) =>
            new UpdateSocialNetworksCommand(id, socialNetworkDtos);
    }
}
