using PetHome.Application.Abstractions;
using PetHome.Application.Dtos;

namespace PetHome.Application.VolunteersManagement.Commands.Create
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
