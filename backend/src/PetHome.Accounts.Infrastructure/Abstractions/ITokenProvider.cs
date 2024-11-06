using PetHome.Accounts.Domain;
using PetHome.Accounts.Infrastructure.Models;
using PetHome.Shared.Core.Shared;
using System.Security.Claims;

namespace PetHome.Accounts.Infrastructure.Abstractions
{
    public interface ITokenProvider
    {
        Task<JwtTokenResult> GenerateAccessToken(User user, CancellationToken cancellationToken);
        Task<Guid> GenerateRefreshToken(User user, Guid accessTokenJti, CancellationToken cancellationToken);
        Task<Result<IReadOnlyList<Claim>>> GetUserClaims(string jwtToken, CancellationToken cancellationToken);
    }
}
