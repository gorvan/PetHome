using PetHome.Shared.Core.Shared;

namespace PetHome.Shared.Core.Dtos;
public record DisscusionDto
{
    public Guid DisscusionId { get; init; }
    public Guid RelationId { get; init; }
    public IReadOnlyList<Guid> Users { get; init; } = [];
    public IReadOnlyList<MessageDto> Messages { get; init; } = [];
    public DisscusionState State { get; init; }
}
