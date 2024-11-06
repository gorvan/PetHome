using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using PetHome.Accounts.Contracts.Responses;
using PetHome.Accounts.Domain;
using PetHome.Accounts.Infrastructure.Abstractions;
using PetHome.Shared.Core.Abstractions;
using PetHome.Shared.Core.Shared;

namespace PetHome.Accounts.Application.AccountsMenagement.Commands.Login;

public class LoginUserHandler : ICommandHandler<LoginResponse, LoginUserCommand>
{
    private readonly UserManager<User> _userManager;
    private readonly ITokenProvider _tokenProvider;
    private readonly ILogger<LoginUserHandler> _logger;
    public LoginUserHandler(
        UserManager<User> userManager,
        ITokenProvider tokenProvider,
        ILogger<LoginUserHandler> logger)
    {
        _userManager = userManager;
        _tokenProvider = tokenProvider;
        _logger = logger;
    }

    public async Task<Result<LoginResponse>> Execute(
        LoginUserCommand command,
        CancellationToken cancellationtoken)
    {
        var user = await _userManager.FindByEmailAsync(command.Email);
        if (user is null)
        {
            return Errors.General.NotFound();
        }

        var passwordConfirmed = await _userManager.CheckPasswordAsync(user, command.Password);
        if (passwordConfirmed is false)
        {
            return Errors.User.InvalidCredentials();
        }

        var accessToken = await _tokenProvider.GenerateAccessToken(user, cancellationtoken);

        var refreshToken = await _tokenProvider.GenerateRefreshToken(user, accessToken.Jti, cancellationtoken);

        _logger.LogInformation("Successfully logged in");

        return new LoginResponse(accessToken.AccessToken, refreshToken);
    }
}
