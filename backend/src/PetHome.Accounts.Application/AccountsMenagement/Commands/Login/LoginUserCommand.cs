using PetHome.Shared.Core.Abstractions;

namespace PetHome.Accounts.Application.AccountsMenagement.Commands.Login
{
    public record LoginUserCommand(string Email, string Password) : ICommand;
}
