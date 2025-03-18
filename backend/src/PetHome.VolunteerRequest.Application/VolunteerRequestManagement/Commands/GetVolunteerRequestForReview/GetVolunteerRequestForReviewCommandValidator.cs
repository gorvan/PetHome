using FluentValidation;
using PetHome.Shared.Core.Shared;
using PetHome.Shared.Core.Validation;

namespace PetHome.VolunteerRequests.Application.VolunteerRequestManagement.Commands.GetVolunteerRequestForReview;
public class GetVolunteerRequestForReviewCommandValidator
    : AbstractValidator<GetVolunteerRequestForReviewCommand>
{
    public GetVolunteerRequestForReviewCommandValidator()
    {
        RuleFor(d => d.VolunteerRequestId).NotEmpty()
               .WithError(Errors.General.ValueIsRequeired());

        RuleFor(d => d.AdminId).NotEmpty()
               .WithError(Errors.General.ValueIsRequeired());

        RuleFor(d => d.VolunteerId).NotEmpty()
               .WithError(Errors.General.ValueIsRequeired());
    }
}
