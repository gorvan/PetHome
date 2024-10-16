using PetHome.Shared.Core.Abstractions;
using PetHome.Shared.Core.Shared;

namespace PetHome.Volunteers.Application.VolunteersManagement.Commands.PetManagement.UpdatePetHelpStatus
{
    public record UpdatePetHelpStatusCommand(
        Guid VolunteerId,
        Guid PetId,
        HelpStatus HelpStatus) : ICommand;
}
