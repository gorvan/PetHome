namespace PetHome.Shared.Core.Dtos;
public record MessageDto
{
    public Guid MessageId { get; init; }
    public Guid UserId { get; init; }
    public string Text { get; init; } = string.Empty;
    public DateTime CreatedAt { get; init; }
    public bool IsEdited { get; init; } = false;
}
