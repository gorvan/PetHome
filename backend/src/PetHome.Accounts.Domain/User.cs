using Microsoft.AspNetCore.Identity;
using PetHome.Accounts.Domain.Accounts;
using PetHome.Shared.Core.Shared;

namespace PetHome.Accounts.Domain;

public class User : IdentityUser<Guid>
{
    private User()
    {
    }

    public List<SocialNetwork> SocialNetworks { get; set; } = [];

    private List<Role> _roles = [];
    public IReadOnlyList<Role> Roles => _roles;

    public ParticipantAccount ParticipantAccount { get; set; }
    public VolunteerAccount? VolunteerAccount { get; set; }


    public static User CreateAmin(string userName, string email, Role role)
    {
        return new User
        {
            UserName = userName,
            Email = email,
            _roles = [role]
        };
    }

    public static User CreateParticipant(string userName, string email, Role role)
    {
        return new User
        {
            UserName = userName,
            Email = email,
            _roles = [role]
        };
    }
}
