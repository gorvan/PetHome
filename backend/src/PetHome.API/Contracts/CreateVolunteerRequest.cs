﻿using PetHome.Application.Dtos;
using PetHome.Application.VolunteersManagement.Commands.Create;

namespace PetHome.API.Contracts
{
    public record CreateVolunteerRequest
    (
        FullNameDto FullName,
        string Email,
        string Description,
        string Phone,
        IEnumerable<SocialNetworkDto> SocialNetworkDtos,
        IEnumerable<RequisiteDto> RequisiteDtos,
        int Experience)
    {
        public CreateVolunteerCommand ToCommand() =>
                new CreateVolunteerCommand(
                     FullName,
                     Email,
                     Description,
                     Phone,
                     SocialNetworkDtos,
                     RequisiteDtos,
                     Experience);
    }
}
