using PetHome.Shared.Core.Shared.IDs;
using PetHome.VolunteerRequests.Domain.Shared;
using PetHome.VolunteerRequests.Domain.ValueObjects;

namespace PetHome.VolunteerRequests.Domain;

public class VolunteerRequest
{
    private VolunteerRequest(
        AdminId adminId,
        UserId userId,
        VolunteerInfo volunteerInfo,
        RequestStatus status,
        DateTime createAt)
    {
        AdminId = adminId;
        UserId = userId;
        VolunteerInfo = volunteerInfo;
        Status = status;
        CreatedAt = createAt;  
    }

    public AdminId AdminId { get; init; }
    public UserId UserId { get; init; }
    public VolunteerInfo VolunteerInfo { get; private set; } = default!;
    public RequestStatus Status { get; private set; }
    public DateTime CreatedAt { get; init; }
    public Comment RejectionComment { get; private set; } = default!;
    public DisscusionId DisscusionId { get; private set; } = default!;

    public static VolunteerRequest Create(
        AdminId adminId,
        UserId userId,
        VolunteerInfo volunteerInfo,
        RequestStatus status,
        DateTime createAt)
    {
        return new VolunteerRequest(
            adminId,
            userId,
            volunteerInfo,
            status,
            createAt);
    }

    public void GetOnReview() =>
        Status = RequestStatus.Submitted;

    public void SendToRevision(Comment comment, DisscusionId disscusionId)
    {
        RejectionComment = comment;
        DisscusionId = disscusionId;
        Status = RequestStatus.Reversion_required;
    }        

    public void RejectRequest() =>
        Status = RequestStatus.Rejected;

    public void ApproveRequest() =>
        Status = RequestStatus.Approved;
}
