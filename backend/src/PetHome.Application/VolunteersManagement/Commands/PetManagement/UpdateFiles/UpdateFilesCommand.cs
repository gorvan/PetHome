using PetHome.Application.Abstractions;
using PetHome.Application.Dtos;

namespace PetHome.Application.VolunteersManagement.Commands.PetManagement.UpdateFiles
{
    public record UpdateFilesCommand(
         Guid VolunteerId,
         Guid petId,
         IEnumerable<FileDto> FilesList) : ICommand;
}
