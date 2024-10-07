using PetHome.Application.Abstractions;

namespace PetHome.Application.VolunteersManagement.Commands.PetManagement.DeletePet
{
    public record DeletePetCommand(
        Guid VolunteerId,
        Guid PetId) : ICommand;
}
