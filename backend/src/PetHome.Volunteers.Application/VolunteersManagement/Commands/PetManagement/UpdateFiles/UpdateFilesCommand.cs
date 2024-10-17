using PetHome.Shared.Core.Abstractions;
using PetHome.Shared.Core.Dtos;

namespace PetHome.Volunteers.Application.VolunteersManagement.Commands.PetManagement.UpdateFiles
{
    public record UpdateFilesCommand(
         Guid VolunteerId,
         Guid petId,
         IEnumerable<FileDto> FilesList) : ICommand;
}
