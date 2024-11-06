using Microsoft.Extensions.DependencyInjection;
using PetHome.Accounts.Contracts.Responses;
using PetHome.Accounts.Infrastructure.Abstractions;
using PetHome.Accounts.Infrastructure.Models;
using PetHome.Shared.Core.Abstractions;
using PetHome.Shared.Core.Extensions;
using PetHome.Shared.Core.Shared;

namespace PetHome.Accounts.Application.AccountsMenagement.Commands.RefreshTokens;

public class RefreshTokensHandler(
    IRefreshSessionManager refreshSessionManager,
    ITokenProvider tokenProvider,
    [FromKeyedServices(nameof(Accounts))] IUnitOfWork unitOfWork)
    : ICommandHandler<LoginResponse, RefreshTokensCommand>
{
    public async Task<Result<LoginResponse>> Execute(
        RefreshTokensCommand command,
        CancellationToken cancellationToken)
    {
        var oldRefreshSession = await refreshSessionManager.GetByRefreshToken(
            command.RefreshToken,
            cancellationToken);

        if (oldRefreshSession.IsFailure)
        {
            return oldRefreshSession.Errors.ToErrorList();
        }

        if (oldRefreshSession.Value.ExpiresIn < DateTime.UtcNow)
        {
            return Errors.Tokens.ExpiredToken();
        }

        var userClaimsResult = await tokenProvider.GetUserClaims(command.AccessToken, cancellationToken);

        if (userClaimsResult.IsFailure)
        {
            return Errors.Tokens.InvalidToken();
        }

        var userIdResultString = userClaimsResult.Value.FirstOrDefault(c => c.Type == CustomClaims.Id)?.Value;
        if (!Guid.TryParse(userIdResultString, out var userId))
        {
            return Errors.General.Failure();
        }

        if (oldRefreshSession.Value.UserId != userId)
        {
            return Errors.Tokens.InvalidToken();
        }

        var userJtiResultString = userClaimsResult.Value.FirstOrDefault(c => c.Type == CustomClaims.Jti)?.Value;
        if (!Guid.TryParse(userJtiResultString, out var userJti))
        {
            return Errors.General.Failure();
        }

        if (oldRefreshSession.Value.Jti != userJti)
        {
            return Errors.Tokens.InvalidToken();
        }

        refreshSessionManager.Delete(oldRefreshSession.Value);
        await unitOfWork.SaveChanges(cancellationToken);

        var accessToken = await tokenProvider
            .GenerateAccessToken(oldRefreshSession.Value.User, cancellationToken);

        var refreshToken = await tokenProvider
            .GenerateRefreshToken(oldRefreshSession.Value.User, accessToken.Jti, cancellationToken);

        return new LoginResponse(accessToken.AccessToken, refreshToken);
    }
}
