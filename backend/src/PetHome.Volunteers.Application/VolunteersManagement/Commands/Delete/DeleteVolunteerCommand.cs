using PetHome.Shared.Core.Abstractions;

namespace PetHome.Volunteers.Application.VolunteersManagement.Commands.Delete
{
    public record DeleteVolunteerCommand(Guid VolunteerId) : ICommand;
}
