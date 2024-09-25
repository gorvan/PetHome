using PetHome.Application.Abstractions;
using PetHome.Application.Dtos;

namespace PetHome.Application.VolunteersManagement.Commands.UpdateMainInfo
{
    public record UpdateMainInfoCommand(
        Guid VolunteerId,
        FullNameDto FullName,
        string Email,
        string Phone,
        string Description) : ICommand;
}
