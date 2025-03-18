using PetHome.Shared.Core.Shared;
using PetHome.VolunteerRequests.Application.VolunteerRequestManagement.Queries.GetVounteerRequestsByVolunteer;

namespace PetHome.VolunteerRequests.Application.Contracts;
public record GetVounteerRequestsByVolunteerRequest(
    int Page,
    int PageSize,
    RequestStatus Status)
{
    public GetVounteerRequestsByVolunteerQuery ToQuery(Guid volunteerId) =>
        new GetVounteerRequestsByVolunteerQuery(volunteerId, Page, PageSize, Status);
}
