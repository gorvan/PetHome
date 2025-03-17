using FluentValidation;
using PetHome.Shared.Core.Shared;
using PetHome.Shared.Core.Validation;

namespace PetHome.VolunteerRequests.Application.VolunteerRequestManagement.Commands.RejectVolunteerRequest;
public class RejectVolunteerRequestCommandValidator : AbstractValidator<RejectVolunteerRequestCommand>
{
    public RejectVolunteerRequestCommandValidator()
    {
        RuleFor(d => d.VolunteerRequestId).NotEmpty()
               .WithError(Errors.General.ValueIsRequeired());
    }
}
