using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using PetHome.Accounts.Domain;
using PetHome.Shared.Core.Abstractions;
using PetHome.Shared.Core.Extensions;
using PetHome.Shared.Core.Shared;

namespace PetHome.Accounts.Application.AccountsMenagement.Commands.Register
{
    public class RegisterUserHandler : ICommandHandler<RegisterUserCommand>
    {
        private readonly UserManager<User> _userManager;
        private readonly ILogger<RegisterUserHandler> _logger;
        public RegisterUserHandler(UserManager<User> userManager, ILogger<RegisterUserHandler> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<Result> Execute(RegisterUserCommand command, CancellationToken token)
        {
            var user = new User
            {
                Email = command.Email,
                UserName = command.UserName,
            };

            var result = await _userManager.CreateAsync(user, command.Password);

            if (result.Succeeded)
            {
                _logger.LogInformation("User created: {userName} a new account with password", command.UserName);
                return Result.Success();
            }

            var errors = result.Errors
                    .Select(e => Error.Failure(e.Code, e.Description))
                    .ToList();

            return errors.ToErrorList();
        }
    }
}
