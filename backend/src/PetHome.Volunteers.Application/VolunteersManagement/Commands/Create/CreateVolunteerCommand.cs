using PetHome.Shared.Core.Abstractions;
using PetHome.Shared.Core.Dtos;

namespace PetHome.Volunteers.Application.VolunteersManagement.Commands.Create
{
    public record CreateVolunteerCommand(
        FullNameDto FullName,
        string Email,
        string Description,
        string Phone,
        IEnumerable<SocialNetworkDto> SocialNetworkDtos,
        IEnumerable<RequisiteDto> RequisiteDtos,
        int Experience) : ICommand;
}
