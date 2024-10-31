using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using PetHome.Accounts.Domain;
using PetHome.Accounts.Domain.Accounts;
using PetHome.Shared.Core.Abstractions;
using PetHome.Shared.Core.Extensions;
using PetHome.Shared.Core.Shared;

namespace PetHome.Accounts.Application.AccountsMenagement.Commands.Register
{
    public class RegisterUserHandler : ICommandHandler<RegisterUserCommand>
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly ILogger<RegisterUserHandler> _logger;
        public RegisterUserHandler(
            UserManager<User> userManager, 
            RoleManager<Role> roleManager, 
            ILogger<RegisterUserHandler> logger)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
        }

        public async Task<Result> Execute(RegisterUserCommand command, CancellationToken token)
        {
            var adminRole = await _roleManager.FindByNameAsync(AdminAccount.ADMIN)
                ?? throw new ApplicationException("Could not find admin role.");

            var user = User.CreateAmin(command.Email, command.UserName, adminRole);

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
