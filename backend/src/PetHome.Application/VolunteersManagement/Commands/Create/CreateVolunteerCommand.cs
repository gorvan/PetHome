using PetHome.Application.Abstractions;
using PetHome.Application.Dtos;

namespace PetHome.Application.VolunteersManagement.Commands.Create
{
    public record CreateVolunteerCommand(
        FullNameDto fullName,
        string email,
        string description,
        string phone,
        IEnumerable<SocialNetworkDto> socialNetworkDtos,
        IEnumerable<RequisiteDto> requisiteDtos) : ICommand;
}
