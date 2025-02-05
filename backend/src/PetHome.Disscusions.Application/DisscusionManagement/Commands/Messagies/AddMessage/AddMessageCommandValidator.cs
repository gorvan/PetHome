using FluentValidation;
using PetHome.Shared.Core.Shared;
using PetHome.Shared.Core.Validation;

namespace PetHome.Disscusions.Application.DisscusionManagement.Commands.Messagies.AddMessage;
public class AddMessageCommandValidator : AbstractValidator<AddMessageCommand>
{
    public AddMessageCommandValidator()
    {
        RuleFor(m => m.DisscusionId).NotEmpty()
            .WithError(Errors.General.ValueIsRequeired());

        RuleFor(m=>m.Message).NotEmpty()
            .WithError(Errors.General.ValueIsRequeired());

        RuleFor(m=>m.UserId).NotEmpty()
            .WithError(Errors.General.ValueIsRequeired());        
    }
}
