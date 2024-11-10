namespace PetHome.Shared.Core.Dtos;

public class RoleDto
{
    public Guid Id { get; init; }
    public string Name { get; init; } = default!;
    public PermissionDto[] Permissions { get; set; } = [];
}

