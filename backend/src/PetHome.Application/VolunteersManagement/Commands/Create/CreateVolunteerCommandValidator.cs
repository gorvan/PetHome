using FluentValidation;
using PetHome.Application.Validation;
using PetHome.Domain.PetManadgement.ValueObjects;
using PetHome.Domain.Shared;

namespace PetHome.Application.VolunteersManagement.Commands.Create
{
    public class CreateVolunteerCommandValidator : AbstractValidator<CreateVolunteerCommand>
    {
        public CreateVolunteerCommandValidator()
        {
            RuleFor(c => c.FullName).MustBeValueObject(x =>
                FullName.Create(x.FirstName, x.SecondName, x.Surname));

            RuleFor(c => c.Email).MustBeValueObject(Email.Create);

            RuleFor(c => c.Phone).MustBeValueObject(Phone.Create);

            RuleFor(c => c.Description).MustBeValueObject(VolunteerDescription.Create);

            RuleForEach(c => c.RequisiteDtos)
                 .MustBeValueObject(x => Requisite.Create(x.Name, x.Description));

            RuleForEach(c => c.SocialNetworkDtos)
                .MustBeValueObject(x => SocialNetwork.Create(x.Name, x.Path));

            RuleFor(c => c.Experience).GreaterThanOrEqualTo(0);
        }
    }
}
