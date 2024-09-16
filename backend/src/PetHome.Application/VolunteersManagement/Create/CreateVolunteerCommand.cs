using PetHome.Application.Volunteers.Shared;

namespace PetHome.Application.VolunteersManagement.Create
{
    public record CreateVolunteerCommand(
        FullNameDto fullName,
        string email,
        string description,
        string phone,
        List<SocialNetworkDto> socialNetworkDtos,
        List<RequisiteDto> requisiteDtos);
}
