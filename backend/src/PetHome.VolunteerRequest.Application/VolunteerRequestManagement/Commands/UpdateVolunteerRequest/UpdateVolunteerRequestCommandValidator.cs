using FluentValidation;
using PetHome.Shared.Core.Shared;
using PetHome.Shared.Core.Validation;

namespace PetHome.VolunteerRequests.Application.VolunteerRequestManagement.Commands.UpdateVolunteerRequest;
public class UpdateVolunteerRequestCommandValidator : AbstractValidator<UpdateVolunteerRequestCommand>
{
    public UpdateVolunteerRequestCommandValidator()
    {
        RuleFor(c => c.VolunteerRequestId).NotEmpty();

        RuleFor(c => c.UserId).NotEmpty();

        RuleFor(c => c.FullName).MustBeValueObject(x =>
                FullName.Create(x.FirstName, x.SecondName, x.Surname));

        RuleFor(c => c.Email).MustBeValueObject(Email.Create);

        RuleFor(c => c.Phone).MustBeValueObject(Phone.Create);

        RuleFor(c => c.Description).MustBeValueObject(DescriptionValueObject.Create);

        RuleFor(c => c.CreateAt).GreaterThan(DateTime.MinValue).LessThan(DateTime.MaxValue);
    }
}
