using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using PetHome.Accounts.Domain;
using PetHome.Accounts.Infrastructure.Abstractions;
using PetHome.Shared.Core.Abstractions;
using PetHome.Shared.Core.Shared;

namespace PetHome.Accounts.Application.AccountsMenagement.Commands.Login;

public class LoginUserHandler : ICommandHandler<string, LoginUserCommand>
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

    public async Task<Result<string>> Execute(
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

        var token = _tokenProvider.GenerateAccessToken(user);

        _logger.LogInformation("Successfully logged in");

        return token;
    }
}
