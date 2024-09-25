using PetHome.Application.Abstractions;
using PetHome.Application.Dtos;

namespace PetHome.Application.VolunteersManagement.Commands.PetManagement.AddPetFiles
{
    public record AddPetFilesCommand(
        Guid VolunteerId,
        Guid petId,
        IEnumerable<FileDto> FilesList) : ICommand;
}
