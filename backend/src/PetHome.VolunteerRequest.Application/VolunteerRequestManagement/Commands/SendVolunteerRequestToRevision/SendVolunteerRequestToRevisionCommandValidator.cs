using FluentValidation;
using PetHome.Shared.Core.Shared;
using PetHome.Shared.Core.Validation;

namespace PetHome.VolunteerRequests.Application.VolunteerRequestManagement.Commands.SendVolunteerRequestToRevision;
public class SendVolunteerRequestToRevisionCommandValidator 
    : AbstractValidator<SendVolunteerRequestToRevisionCommand>
{
    public SendVolunteerRequestToRevisionCommandValidator()
    {
        RuleFor(d => d.VolunteerRequestId).NotEmpty()
               .WithError(Errors.General.ValueIsRequeired());

        RuleFor(d => d.AdminId).NotEmpty()
               .WithError(Errors.General.ValueIsRequeired());

        RuleFor(d => d.DisscusionId).NotEmpty()
               .WithError(Errors.General.ValueIsRequeired());

        RuleFor(d => d.Comment).NotEmpty()
               .WithError(Errors.General.ValueIsRequeired());
    }
}
