using FluentValidation;
using PetHome.Shared.Core.Shared;
using PetHome.Shared.Core.Validation;

namespace PetHome.Disscusions.Application.DisscusionManagement.Commands.CreateDisscusion;
public class CreateDisscusionCommandValidator : AbstractValidator<CreateDisscusionCommand>
{
    public CreateDisscusionCommandValidator()
    {
        RuleFor(d => d.RelationId).NotEmpty()
               .WithError(Errors.General.ValueIsRequeired());

        RuleFor(d => d.UserIds).Must(x => x.Count > 1);              
    }
}
