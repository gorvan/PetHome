using PetHome.Shared.Core.Shared.IDs;
using PetHome.VolunteerRequests.Domain.Shared;
using PetHome.VolunteerRequests.Domain.ValueObjects;

namespace PetHome.VolunteerRequests.Domain;

public class VolunteerRequest
{
    private VolunteerRequest(
        RequestId requestId,
        UserId userId,
        VolunteerInfo volunteerInfo,
        RequestStatus status,
        DateTime createAt)
    {
        RequestId = requestId;
        UserId = userId;
        VolunteerInfo = volunteerInfo;
        Status = status;
        CreatedAt = createAt;  
    }

    public RequestId RequestId { get; set; }
    public AdminId AdminId { get; private set; } = default!;
    public UserId UserId { get; init; }
    public VolunteerInfo VolunteerInfo { get; private set; } = default!;
    public RequestStatus Status { get; private set; }
    public DateTime CreatedAt { get; init; }
    public Comment RejectionComment { get; private set; } = default!;
    public DisscusionId DisscusionId { get; private set; } = default!;

    public static VolunteerRequest Create(
        RequestId requestId,
        UserId userId,
        VolunteerInfo volunteerInfo,
        RequestStatus status,
        DateTime createAt)
    {
        return new VolunteerRequest(
            requestId,
            userId,
            volunteerInfo,
            status,
            createAt);
    }

    public void GetOnReview(AdminId adminId)
    {
        AdminId = adminId;
        Status = RequestStatus.Submitted;
    }        

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
