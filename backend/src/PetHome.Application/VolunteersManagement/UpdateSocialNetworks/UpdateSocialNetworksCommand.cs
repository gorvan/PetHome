﻿using PetHome.Application.Volunteers.Shared;

namespace PetHome.Application.VolunteersManagement.UpdateSocialNetworks
{
    public record UpdateSocialNetworksCommand(
        Guid VolunteerId, 
        List<SocialNetworkDto> SocialNetworks);
}