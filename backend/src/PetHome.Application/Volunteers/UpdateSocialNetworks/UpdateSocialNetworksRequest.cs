using PetHome.Application.Volunteers.Shared;

namespace PetHome.Application.Volunteers.UpdateSocialNetworks
{
    public record UpdateSocialNetworksRequest(
        Guid VolunteerId, 
        List<SocialNetworkDto> SocialNetworks);
}
