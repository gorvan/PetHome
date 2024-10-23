using PetHome.Accounts.Application.AccountsMenagement.Commands.Login;

namespace PetHome.Accounts.Application.Contracts
{
    public record LoginUserRequest(string Email, string Password)
    {
        public LoginUserCommand ToCommand() =>
            new LoginUserCommand(Email, Password);
    }
}
