using PetHome.Accounts.Domain.Accounts;

namespace PetHome.Accounts.Infrastructure.IdentityManager
{
    public class AdminAccountManager(AccountsDbContext accountsContext)
    {
        public async Task CreateAdminAccount(AdminAccount adminAccount)
        {
            await accountsContext.AdminAccounts.AddAsync(adminAccount);
            await accountsContext.SaveChangesAsync();
        }
    }
}
