using PetHome.Shared.Core.Abstractions;

namespace PetHome.VolunteerRequests.Application.VolunteerRequestManagement.Commands.GetVolunteerRequestForReview;
public record GetVolunteerRequestForReviewCommand(
    Guid VolunteerRequestId,
    Guid AdminId,
    Guid VolunteerId) : ICommand;
