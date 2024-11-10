namespace PetHome.Shared.Core.Dtos;
public class AccountDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = default!;
    public string SecondName { get; set; } = default!;
    public string Surname { get; set; } = default!;
}

public class UserDto
{
    public Guid Id { get; init; }
    public string Name { get; init; } = default!;
    public SocialNetworkDto[] SocialNetworks { get; set; } = [];
    public RoleDto[] Roles { get; set; } = [];
    public AccountDto Participant { get; init; } = default!;
    public AccountDto Volunteer { get; init; } = default!;
}

public class RoleDto
{
    public Guid Id { get; init; }
    public string Name { get; init; } = default!;
    public PermissionDto[] Permissions { get; set; } = [];
}

public class PermissionDto
{
    public Guid Id { get; init; }
    public string Code { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
}

