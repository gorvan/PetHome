using PetHome.Application.Volunteers.Shared;

namespace PetHome.Application.Volunteers.UpdateRequisites
{
    public record UpdateRequisitesCommand(Guid VolunteerId,
        List<RequisiteDto> Requisites);
}
