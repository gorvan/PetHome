﻿using PetHome.Application.Volunteers.Shared;

namespace PetHome.Application.Volunteers.Create
{
    public record CreateVolunteerCommand(
        FullNameDto fullName,
        string email,
        string description,
        string phone,
        List<SocialNetworkDto> socialNetworkDtos,
        List<RequisiteDto> requisiteDtos);
}