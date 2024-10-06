using PetHome.Application.Abstractions;
using PetHome.Domain.Shared;

namespace PetHome.Application.VolunteersManagement.Commands.PetManagement.UpdatePetHelpStatus
{
    public record UpdatePetHelpStatusCommand(
        Guid VolunteerId,
        Guid PetId,
        HelpStatus HelpStatus) : ICommand;
}
