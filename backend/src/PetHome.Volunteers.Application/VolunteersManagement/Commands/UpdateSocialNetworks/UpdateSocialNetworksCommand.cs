using PetHome.Shared.Core.Abstractions;
using PetHome.Shared.Core.Dtos;

namespace PetHome.Volunteers.Application.VolunteersManagement.Commands.UpdateSocialNetworks
{
    public record UpdateSocialNetworksCommand(
        Guid VolunteerId,
        IEnumerable<SocialNetworkDto> SocialNetworks) : ICommand;
}
