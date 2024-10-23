using PetHome.Accounts.Application.AccountsMenagement.Commands.Register;

namespace PetHome.Accounts.Application.Contracts
{
    public record RegisterUserRequest(string Email, string UserName, string Password)
    {
        public RegisterUserCommand ToCommand() =>
            new(Email, UserName, Password);
    }
}
