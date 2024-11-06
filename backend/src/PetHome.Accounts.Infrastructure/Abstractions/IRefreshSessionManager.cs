using PetHome.Accounts.Domain;
using PetHome.Shared.Core.Shared;

namespace PetHome.Accounts.Infrastructure.Abstractions
{
    public interface IRefreshSessionManager
    {
        Task<Result<RefreshSession>> GetByRefreshToken(
            Guid refreshToken,
            CancellationToken cancellationToken);

        void Delete(RefreshSession refreshSession);
    }
}