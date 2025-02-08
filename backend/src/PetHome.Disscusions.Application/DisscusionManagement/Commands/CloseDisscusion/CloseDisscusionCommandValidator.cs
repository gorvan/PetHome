using FluentValidation;
using PetHome.Shared.Core.Shared;
using PetHome.Shared.Core.Validation;

namespace PetHome.Disscusions.Application.DisscusionManagement.Commands.CloseDisscusion;
public class CloseDisscusionCommandValidator : AbstractValidator<CloseDisscusionCommand>
{
    public CloseDisscusionCommandValidator()
    {
        RuleFor(d => d.DisscusionId).NotEmpty()
               .WithError(Errors.General.ValueIsRequeired());
    }
}
