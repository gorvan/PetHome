using PetHome.Application.VolunteersManagement.Commands.PetManagement.UpdatePetHelpStatus;
using PetHome.Domain.Shared;

namespace PetHome.API.Contracts
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
