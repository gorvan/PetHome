using PetHome.Application.Dtos;
using PetHome.Application.VolunteersManagement.Commands.Create;

namespace PetHome.API.Contracts
{
    public record CreateVolunteerRequest
    (
        FullNameDto fullName,
        string email,
        string description,
        string phone,
        IEnumerable<SocialNetworkDto> socialNetworkDtos,
        IEnumerable<RequisiteDto> requisiteDtos)
    {
        public CreateVolunteerCommand ToCommand() =>
                new CreateVolunteerCommand(
                     fullName,
                     email,
                     description,
                     phone,
                     socialNetworkDtos,
                     requisiteDtos);
    }
}
