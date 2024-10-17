using PetHome.Shared.Core.Abstractions;

namespace PetHome.Volunteers.Application.VolunteersManagement.Commands.PetManagement.DeletePet
{
    public record DeletePetCommand(
        Guid VolunteerId,
        Guid PetId) : ICommand;
}
