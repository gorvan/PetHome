using FluentValidation;
using PetHome.Shared.Core.Shared;
using PetHome.Shared.Core.Validation;

namespace PetHome.Disscusions.Application.DisscusionManagement.Commands.Messagies.DeleteMessage;
public class DeleteMessageCommandValidator : AbstractValidator<DeleteMessageCommand>
{
    public DeleteMessageCommandValidator()
    {
        RuleFor(m => m.DisscusionId).NotEmpty()
            .WithError(Errors.General.ValueIsRequeired());

        RuleFor(m => m.MessageId).NotEmpty()
            .WithError(Errors.General.ValueIsRequeired());

        RuleFor(m => m.UserId).NotEmpty()
            .WithError(Errors.General.ValueIsRequeired());
    }
}
