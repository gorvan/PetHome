using PetHome.Shared.Core.Shared;

namespace PetHome.Accounts.Domain.Accounts
{
    public class AdminAccount
    {
        public const string ADMIN = nameof(ADMIN);

        private AdminAccount() { }

        public AdminAccount(FullName fullName, User user)
        {
            Id = Guid.NewGuid();
            User = user;
            UserId = user.Id;
            FullName = fullName;
        }

        public Guid Id { get; set; }
        public User User { get; set; }
        public Guid UserId { get; set; }
        public FullName FullName { get; set; }
    }
}
