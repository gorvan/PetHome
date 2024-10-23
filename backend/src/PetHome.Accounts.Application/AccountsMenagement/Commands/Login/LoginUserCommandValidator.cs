using FluentValidation;
using PetHome.Shared.Core.Shared;
using PetHome.Shared.Core.Validation;

namespace PetHome.Accounts.Application.AccountsMenagement.Commands.Login
{
    public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
    {
        public LoginUserCommandValidator()
        {
            RuleFor(l => l.Email).NotEmpty()
                    .WithError(Errors.General.ValueIsRequeired());

            RuleFor(l => l.Password).NotEmpty()
                   .WithError(Errors.General.ValueIsRequeired());
        }
    }
}
