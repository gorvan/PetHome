using FluentValidation;
using PetHome.Shared.Core.Shared;
using PetHome.Shared.Core.Validation;

namespace PetHome.Accounts.Application.AccountsMenagement.Commands.Register
{
    public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator()
        {
            RuleFor(l => l.Email).NotEmpty()
                   .WithError(Errors.General.ValueIsRequeired());

            RuleFor(l => l.UserName).NotEmpty()
                   .WithError(Errors.General.ValueIsRequeired());

            RuleFor(l => l.Password).NotEmpty()
                   .WithError(Errors.General.ValueIsRequeired());
        }
    }
}
