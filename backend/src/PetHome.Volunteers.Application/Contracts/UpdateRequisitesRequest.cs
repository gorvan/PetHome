using PetHome.Shared.Core.Dtos;
using PetHome.Volunteers.Application.VolunteersManagement.Commands.UpdateRequisites;

namespace PetHome.Volunteers.Application.Contracts
{
    public record UpdateRequisitesRequest(
        IEnumerable<RequisiteDto> RequisitesList
    )
    {
        public UpdateRequisitesCommand ToCommand(Guid id) =>
                new UpdateRequisitesCommand(id, RequisitesList);
    }
}
