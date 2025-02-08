using PetHome.VolunteerRequests.Application.VolunteerRequestManagement.Queries.GetVolunteerRequestForReview;

namespace PetHome.VolunteerRequests.Application.Contracts;
public record GetVolunteerRequestForReview_Request()
{
    public GetVolunteerRequestForReviewQuery ToCommand() =>
        new GetVolunteerRequestForReviewQuery();
}
