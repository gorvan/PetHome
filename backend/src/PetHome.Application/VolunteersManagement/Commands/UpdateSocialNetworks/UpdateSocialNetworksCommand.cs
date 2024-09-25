using PetHome.Application.Abstractions;
using PetHome.Application.Dtos;

namespace PetHome.Application.VolunteersManagement.Commands.UpdateSocialNetworks
{
    public record UpdateSocialNetworksCommand(
        Guid VolunteerId,
        IEnumerable<SocialNetworkDto> SocialNetworks) : ICommand;
}
