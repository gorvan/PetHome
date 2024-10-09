using PetHome.Application.Abstractions;

namespace PetHome.Application.VolunteersManagement.Commands.PetManagement.FullDeletePet
{
    public record FullDeletePetCommand(
        Guid VolunteerId,
        Guid PetId) : ICommand;
}
