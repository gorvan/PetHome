using PetHome.Shared.Core.Abstractions;

namespace PetHome.Volunteers.Application.VolunteersManagement.Commands.PetManagement.FullDeletePet
{
    public record FullDeletePetCommand(
        Guid VolunteerId,
        Guid PetId) : ICommand;
}
