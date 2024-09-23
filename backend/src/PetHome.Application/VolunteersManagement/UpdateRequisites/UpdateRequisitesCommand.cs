using PetHome.Application.Volunteers.Shared;

namespace PetHome.Application.VolunteersManagement.UpdateRequisites
{
    public record UpdateRequisitesCommand(Guid VolunteerId,
        IEnumerable<RequisiteDto> Requisites);
}
