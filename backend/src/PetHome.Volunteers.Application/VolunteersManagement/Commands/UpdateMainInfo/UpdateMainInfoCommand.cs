using PetHome.Shared.Core.Abstractions;
using PetHome.Shared.Core.Dtos;

namespace PetHome.Volunteers.Application.VolunteersManagement.Commands.UpdateMainInfo
{
    public record UpdateMainInfoCommand(
        Guid VolunteerId,
        FullNameDto FullName,
        string Email,
        string Phone,
        string Description) : ICommand;
}
