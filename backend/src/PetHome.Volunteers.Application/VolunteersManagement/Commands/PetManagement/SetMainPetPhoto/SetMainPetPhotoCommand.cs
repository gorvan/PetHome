using PetHome.Shared.Core.Abstractions;

namespace PetHome.Volunteers.Application.VolunteersManagement.Commands.PetManagement.SetMainPetPhoto
{
    public record SetMainPetPhotoCommand(
        Guid VolunteerId,
        Guid PetId,
        string FilePath) : ICommand;
}
