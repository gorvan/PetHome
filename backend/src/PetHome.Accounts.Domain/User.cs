using Microsoft.AspNetCore.Identity;

namespace PetHome.Accounts.Domain
{
    public class User : IdentityUser<Guid>
    {
        private User()
        {
        }

        public List<SocialNetwork> SocialNetworks { get; set; } = [];

        private List<Role> _roles = [];
        public IReadOnlyList<Role> Roles => _roles;

        public static User CreateAmin(string userName, string email, Role role)
        {
            return new User
            {
                UserName = userName,
                Email = email,
                _roles = [role]
            };
        }
    }
}
