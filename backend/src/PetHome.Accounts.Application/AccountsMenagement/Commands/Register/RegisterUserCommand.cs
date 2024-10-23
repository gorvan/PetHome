using PetHome.Shared.Core.Abstractions;

namespace PetHome.Accounts.Application.AccountsMenagement.Commands.Register
{
    public record RegisterUserCommand(
        string Email, 
        string UserName, 
        string Password) : ICommand;   
}
