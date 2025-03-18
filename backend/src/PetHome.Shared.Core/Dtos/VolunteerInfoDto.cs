namespace PetHome.Shared.Core.Dtos;

public record VolunteerInfoDto
{
    public FullNameDto FullName { get; init; } = default!;
    public string Email { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public string Phone { get; init; } = string.Empty;
}
