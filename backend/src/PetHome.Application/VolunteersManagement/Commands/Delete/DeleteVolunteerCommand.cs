using PetHome.Application.Abstractions;

namespace PetHome.Application.VolunteersManagement.Commands.Delete
{
    public record DeleteVolunteerCommand(Guid VolunteerId) : ICommand;
}
