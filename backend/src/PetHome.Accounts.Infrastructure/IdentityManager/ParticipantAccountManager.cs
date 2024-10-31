using PetHome.Accounts.Domain.Accounts;

namespace PetHome.Accounts.Infrastructure.IdentityManager
{
    public class ParticipantAccountManager(AccountsDbContext accountsContext)
    {
        public async Task CreateParticipantAccount(ParticipantAccount participantAccount)
        {
            await accountsContext.ParticipantAccounts.AddAsync(participantAccount);
        }
    }
}
