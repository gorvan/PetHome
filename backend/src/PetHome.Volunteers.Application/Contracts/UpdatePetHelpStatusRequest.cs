using PetHome.Shared.Core.Shared;
using PetHome.Volunteers.Application.VolunteersManagement.Commands.PetManagement.UpdatePetHelpStatus;

namespace PetHome.Volunteers.Application.Contracts
{
    public record UpdatePetHelpStatusRequest(string HelpStatus)
    {
        public UpdatePetHelpStatusCommand ToCommand(
            Guid VolunteerId,
            Guid PetId)
        {
            return new UpdatePetHelpStatusCommand(
                VolunteerId,
                PetId,
                (HelpStatus)Enum.Parse(typeof(HelpStatus), HelpStatus));
        }
    }
}
