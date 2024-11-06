using Microsoft.EntityFrameworkCore;
using PetHome.Accounts.Domain;
using PetHome.Accounts.Infrastructure.Abstractions;
using PetHome.Shared.Core.Shared;

namespace PetHome.Accounts.Infrastructure.IdentityManager
{
    public class RefreshSessionManager(AccountsDbContext accountsDbContext) : IRefreshSessionManager
    {
        public async Task<Result<RefreshSession>> GetByRefreshToken(
            Guid refreshToken,
            CancellationToken cancellationToken)
        {
            var refreshSession = await accountsDbContext.RefreshSessions
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.RefreshToken == refreshToken, cancellationToken);

            if (refreshSession is null)
            {
                return Errors.General.NotFound(refreshToken);
            }

            return refreshSession;
        }

        public void Delete(RefreshSession refreshSession)
        {
            accountsDbContext.RefreshSessions.Remove(refreshSession);
        }
    }
}
