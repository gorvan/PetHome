using PetHome.Shared.Core.Abstractions;
using PetHome.Shared.Core.Dtos;

namespace PetHome.Volunteers.Application.VolunteersManagement.Commands.PetManagement.AddPetFiles
{
    public record AddPetFilesCommand(
        Guid VolunteerId,
        Guid petId,
        IEnumerable<FileDto> FilesList) : ICommand;
}
