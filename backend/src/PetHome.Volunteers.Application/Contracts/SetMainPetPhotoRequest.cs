using PetHome.Volunteers.Application.VolunteersManagement.Commands.PetManagement.SetMainPetPhoto;

namespace PetHome.Volunteers.Application.Contracts
{
    public record SetMainPetPhotoRequest(string FilePath)
    {
        public SetMainPetPhotoCommand ToCommand(Guid volunteerId, Guid petId)
        {
            return new SetMainPetPhotoCommand(volunteerId, petId, FilePath);
        }
    }
}
