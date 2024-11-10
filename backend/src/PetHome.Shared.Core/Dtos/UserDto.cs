namespace PetHome.Shared.Core.Dtos;

public class UserDto
{
    public Guid Id { get; init; }
    public string Name { get; init; } = default!;
    public SocialNetworkDto[] SocialNetworks { get; set; } = [];
    public RoleDto[] Roles { get; set; } = [];
    public AccountDto Participant { get; init; } = default!;
    public AccountDto Volunteer { get; init; } = default!;
}

