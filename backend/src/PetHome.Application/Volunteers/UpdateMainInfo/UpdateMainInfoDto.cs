using PetHome.Application.Volunteers.Shared;

namespace PetHome.Application.Volunteers.UpdateMainInfo
{
    public record UpdateMainInfoDto(
    FullNameDto FullName,
    string Email,
    string Phone,
    string Description);
}
