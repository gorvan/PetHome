using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using PetHome.Accounts.Domain;
using PetHome.Accounts.Domain.Accounts;
using PetHome.Accounts.Infrastructure.IdentityManager;
using PetHome.Shared.Core.Abstractions;
using PetHome.Shared.Core.Extensions;
using PetHome.Shared.Core.Shared;

namespace PetHome.Accounts.Application.AccountsMenagement.Commands.Register
{
    public class RegisterUserHandler(
        UserManager<User> userManager,
        RoleManager<Role> roleManager,
        ParticipantAccountManager participantAccountManager,
        ILogger<RegisterUserHandler> logger) : ICommandHandler<RegisterUserCommand>
    {       

        public async Task<Result> Execute(RegisterUserCommand command, CancellationToken token)
        {
            var participantRole = await roleManager.FindByNameAsync(ParticipantAccount.PARTICIPANT)
                ?? throw new ApplicationException("Could not find participant role.");

            var user = User.CreateParticipant(command.UserName, command.Email, participantRole);

            var result = await userManager.CreateAsync(user, command.Password);

            if (result.Succeeded)
            {
                logger.LogInformation("User created: {userName} a new account with password",
                    command.UserName);

                var fullName = FullName.Create(
                        command.UserName,
                        command.UserName,
                        command.UserName)
                    .Value;

                var participantAccount = new ParticipantAccount(fullName, user);

                await participantAccountManager.CreateParticipantAccount(participantAccount);

                return Result.Success();
            }

            var errors = result.Errors
                    .Select(e => Error.Failure(e.Code, e.Description))
                    .ToList();

            return errors.ToErrorList();
        }
    }
}
