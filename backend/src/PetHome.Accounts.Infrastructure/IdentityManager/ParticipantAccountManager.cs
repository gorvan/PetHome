using PetHome.Accounts.Domain.Accounts;

namespace PetHome.Accounts.Infrastructure.IdentityManager
{
    public class ParticipantAccountManager(AccountsDbContext accountsContext)
    {
        public async Task CreateParticipantAccount(ParticipantAccount participantAccount)
        {
            try
            {
                await accountsContext.ParticipantAccounts.AddAsync(participantAccount);
                await accountsContext.SaveChangesAsync();
            }
            catch(Exception ex)
            {

            }            
        }
    }
}
