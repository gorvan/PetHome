using PetHome.Shared.Core.Shared;

namespace PetHome.Shared.Core.Dtos;
public record VolunteerRequestDto
{
    public Guid RequestId { get; init; }
    public Guid AdminId { get; init; }
    public Guid UserId { get; init; }
    public VolunteerInfoDto VolunteerInfo { get; init; } = default!;
    public RequestStatus Status { get; init; }
    public DateTime CreatedAt { get; init; }
    public string RejectionComment { get; init; } = string.Empty;
    public Guid DisscusionId { get; init; }
}
