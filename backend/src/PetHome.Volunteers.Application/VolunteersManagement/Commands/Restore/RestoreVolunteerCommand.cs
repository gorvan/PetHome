using PetHome.Shared.Core.Abstractions;

namespace PetHome.Volunteers.Application.VolunteersManagement.Commands.Restore
{
    public record RestoreVolunteerCommand(Guid VolunteerId) : ICommand;
}
