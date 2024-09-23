using PetHome.Application.Volunteers.Shared;
using PetHome.Application.VolunteersManagement.UpdateRequisites;

namespace PetHome.API.Contracts
{
    public record UpdateRequisitesRequest(
        IEnumerable<RequisiteDto> requisitesList
    )
    {
        public UpdateRequisitesCommand ToCommand(Guid id) =>
            new UpdateRequisitesCommand(
                id,
                requisitesList);
    }
}
