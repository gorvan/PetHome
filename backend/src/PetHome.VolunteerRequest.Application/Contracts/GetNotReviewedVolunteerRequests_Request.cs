using PetHome.VolunteerRequests.Application.VolunteerRequestManagement.Queries.GetNotReviewedVolunteerRequests;

namespace PetHome.VolunteerRequests.Application.Contracts;
public record GetNotReviewedVolunteerRequests_Request(int Page, int PageSize)
{
    public GetNotReviewedVolunteerRequestsQuery ToQuery(Guid adminId) =>
        new GetNotReviewedVolunteerRequestsQuery(adminId, Page, PageSize);
}
