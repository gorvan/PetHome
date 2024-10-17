using PetHome.Shared.Core.Dtos;
using PetHome.Volunteers.Application.VolunteersManagement.Commands.UpdateSocialNetworks;

namespace PetHome.Volunteers.Application.Contracts
{
    public record UpdateSocialNetworksRequest(
        IEnumerable<SocialNetworkDto> socialNetworkDtos)
    {
        public UpdateSocialNetworksCommand ToCommand(Guid id) =>
            new UpdateSocialNetworksCommand(id, socialNetworkDtos);
    }
}
