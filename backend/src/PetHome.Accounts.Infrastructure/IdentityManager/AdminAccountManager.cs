using Microsoft.EntityFrameworkCore;
using PetHome.Accounts.Domain.Accounts;

namespace PetHome.Accounts.Infrastructure.IdentityManager
{
    public class AdminAccountManager(AccountsDbContext accountsContext)
    {
        public async Task CreateAdminAccount(AdminAccount adminAccount)
        {
            await accountsContext.AdminAccounts.AddAsync(adminAccount);            
        }

        public async Task<bool> IsAdminAccountExist(string userName, string email)
        {
            return await accountsContext.AdminAccounts
                .AnyAsync(a => a.User.UserName == userName && a.User.Email == email);
        }
    }
}
