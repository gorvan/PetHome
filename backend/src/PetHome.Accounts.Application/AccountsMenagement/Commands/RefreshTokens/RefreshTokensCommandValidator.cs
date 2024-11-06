using FluentValidation;
using PetHome.Shared.Core.Shared;
using PetHome.Shared.Core.Validation;

namespace PetHome.Accounts.Application.AccountsMenagement.Commands.RefreshTokens
{
    public class RefreshTokensCommandValidator : AbstractValidator<RefreshTokensCommand>
    {
        public RefreshTokensCommandValidator()
        {
            RuleFor(l => l.AccessToken).NotEmpty()
                    .WithError(Errors.General.ValueIsRequeired());

            RuleFor(l => l.RefreshToken).NotEmpty()
                   .WithError(Errors.General.ValueIsRequeired());
        }
    }
}
