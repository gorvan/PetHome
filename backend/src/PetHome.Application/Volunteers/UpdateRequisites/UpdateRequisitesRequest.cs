using PetHome.Application.Volunteers.Shared;

namespace PetHome.Application.Volunteers.UpdateRequisites
{
    public record UpdateRequisitesRequest(Guid VolunteerId,
        List<RequisiteDto> Requisites);
}
