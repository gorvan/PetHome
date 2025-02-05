using FluentValidation;
using PetHome.Shared.Core.Shared;
using PetHome.Shared.Core.Validation;

namespace PetHome.Disscusions.Application.DisscusionManagement.Commands.Messagies.EditMessage;
public class EditMessageCommandValidator : AbstractValidator<EditMessageCommand>
{
    public EditMessageCommandValidator()
    {
        RuleFor(m => m.DisscusionId).NotEmpty()
            .WithError(Errors.General.ValueIsRequeired());

        RuleFor(m => m.MessageId).NotEmpty()
            .WithError(Errors.General.ValueIsRequeired());

        RuleFor(m => m.NewMessage).NotEmpty()
            .WithError(Errors.General.ValueIsRequeired());

        RuleFor(m => m.UserId).NotEmpty()
            .WithError(Errors.General.ValueIsRequeired());
    }
}
