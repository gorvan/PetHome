using PetHome.Application.Volunteers.Shared;

namespace PetHome.Application.Volunteers.UpdateSocialNetworks
{
    public record UpdateSocialNetworksCommand(
        Guid VolunteerId, 
        List<SocialNetworkDto> SocialNetworks);
}
