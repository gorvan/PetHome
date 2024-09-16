using PetHome.Application.FileProvider;
using PetHome.Application.Volunteers.Shared;

namespace PetHome.Application.VolunteersManagement.PetManagement.AddPetFiles
{
    public record AddPetFilesCommand(
        Guid VolunteerId,
        Guid petId,
        IEnumerable<FileDto> FilesList);
}
