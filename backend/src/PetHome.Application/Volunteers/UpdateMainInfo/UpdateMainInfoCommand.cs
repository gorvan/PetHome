using PetHome.Application.Volunteers.Shared;

namespace PetHome.Application.Volunteers.UpdateMainInfo
{
    public record UpdateMainInfoCommand(
        Guid VolunteerId,
        FullNameDto FullName,
        string Email,
        string Phone,
        string Description);
}
