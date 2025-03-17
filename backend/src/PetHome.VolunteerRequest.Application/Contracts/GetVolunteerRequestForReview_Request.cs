using PetHome.VolunteerRequests.Application.VolunteerRequestManagement.Commands.GetVolunteerRequestForReview;

namespace PetHome.VolunteerRequests.Application.Contracts;
public record GetVolunteerRequestForReview_Request(Guid VolunteerId)
{
    public GetVolunteerRequestForReviewCommand ToCommand(Guid VolunteerRequestId, Guid AdminId) =>
        new GetVolunteerRequestForReviewCommand(
            VolunteerRequestId,
            AdminId,
            VolunteerId);
}
