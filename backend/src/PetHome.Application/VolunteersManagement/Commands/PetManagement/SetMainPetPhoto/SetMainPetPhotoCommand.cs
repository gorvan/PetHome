using PetHome.Application.Abstractions;

namespace PetHome.Application.VolunteersManagement.Commands.PetManagement.SetMainPetPhoto
{
    public record SetMainPetPhotoCommand(
        Guid VolunteerId,
        Guid PetId,
        string FilePath) : ICommand;
}
