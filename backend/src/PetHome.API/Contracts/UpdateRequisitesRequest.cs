using PetHome.Application.Dtos;
using PetHome.Application.VolunteersManagement.Commands.UpdateRequisites;

namespace PetHome.API.Contracts
{
    public record UpdateRequisitesRequest(
        IEnumerable<RequisiteDto> RequisitesList
    )
    {
        public UpdateRequisitesCommand ToCommand(Guid id) =>
                new UpdateRequisitesCommand(id, RequisitesList);
    }
}
