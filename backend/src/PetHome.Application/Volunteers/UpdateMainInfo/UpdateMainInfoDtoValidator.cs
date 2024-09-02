using FluentValidation;
using PetHome.Application.Validation;
using PetHome.Domain.PetManadgement.ValueObjects;
using PetHome.Domain.Shared;

namespace PetHome.Application.Volunteers.UpdateMainInfo
{
    public class UpdateMainInfoDtoValidator
        : AbstractValidator<UpdateMainInfoDto>
    {
        public UpdateMainInfoDtoValidator()
        {
            RuleFor(u => u.FullName).MustBeValueObject(x =>
                FullName.Create(x.FirstName, x.SecondName, x.Surname));

            RuleFor(u => u.Email).MustBeValueObject(Email.Create);

            RuleFor(u => u.Phone).MustBeValueObject(Phone.Create);

            RuleFor(u => u.Description)
                .MustBeValueObject(VolunteerDescription.Create);
        }
    }
}
