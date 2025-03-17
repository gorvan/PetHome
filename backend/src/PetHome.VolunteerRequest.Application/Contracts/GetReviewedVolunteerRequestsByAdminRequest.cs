using PetHome.Shared.Core.Shared;
using PetHome.VolunteerRequests.Application.VolunteerRequestManagement.Queries.GetReviewedVolunteerRequestsByAdmin;

namespace PetHome.VolunteerRequests.Application.Contracts;
public record GetReviewedVolunteerRequestsByAdminRequest(
    int Page,
    int PageSize,
    RequestStatus Status)
{
    public GetReviewedVolunteerRequestsByAdminQuery ToQuery(Guid adminId) =>
        new GetReviewedVolunteerRequestsByAdminQuery(adminId, Page, PageSize, Status);
}
