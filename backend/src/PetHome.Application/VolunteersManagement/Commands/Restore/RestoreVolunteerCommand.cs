using PetHome.Application.Abstractions;

namespace PetHome.Application.VolunteersManagement.Commands.Restore
{
    public record RestoreVolunteerCommand(Guid VolunteerId) : ICommand;
}
