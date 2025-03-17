using FluentValidation;
using PetHome.Shared.Core.Shared;
using PetHome.Shared.Core.Validation;

namespace PetHome.VolunteerRequests.Application.VolunteerRequestManagement.Commands.AproveVolunteerRequest;
public class AproveVolunteerRequestCommandValidator 
    : AbstractValidator<AproveVolunteerRequestCommand>
{
    public AproveVolunteerRequestCommandValidator()
    {
        RuleFor(d => d.VolunteerRequestId).NotEmpty()
               .WithError(Errors.General.ValueIsRequeired());
    }
}
