using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
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
        [FromKeyedServices(nameof(Accounts))] IUnitOfWork unitOfWork,
        ILogger<RegisterUserHandler> logger) : ICommandHandler<RegisterUserCommand>
    {

        public async Task<Result> Execute(RegisterUserCommand command, CancellationToken token)
        {
            var transaction = await unitOfWork.BeginTransaction(token);

            var participantRole = await roleManager.FindByNameAsync(ParticipantAccount.PARTICIPANT)
                ?? throw new ApplicationException("Could not find participant role.");

            var user = User.CreateParticipant(command.UserName, command.Email, participantRole);

            try
            {
                var result = await userManager.CreateAsync(user, command.Password);

                if (result.Succeeded is false)
                {
                    await userManager.DeleteAsync(user);
                    transaction.Rollback();

                    var errors = result.Errors
                            .Select(e => Error.Failure(e.Code, e.Description))
                            .ToList();
                    
                    return errors.ToErrorList();
                }

                logger.LogInformation("User created: {userName} a new account with password",
                    command.UserName);

                var fullName = FullName.Create(
                        command.UserName,
                        command.UserName,
                        command.UserName)
                    .Value;

                var participantAccount = new ParticipantAccount(fullName, user);

                await participantAccountManager.CreateParticipantAccount(participantAccount);
                
                await unitOfWork.SaveChanges(token);
                transaction.Commit();

                return Result.Success();
            }
            catch(Exception ex)
            {
                await userManager.DeleteAsync(user);

                transaction.Rollback();

                return Error.Failure("user.register", "Fail to register user");                
            }            
        }
    }
}
