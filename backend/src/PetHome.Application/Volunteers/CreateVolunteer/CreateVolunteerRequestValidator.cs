using FluentValidation;
using PetHome.Application.Validation;
using PetHome.Domain.PetManadgement.ValueObjects;
using PetHome.Domain.Shared;

namespace PetHome.Application.Volunteers.CreateVolunteer
{
    public class CreateVolunteerRequestValidator : AbstractValidator<CreateVolunteerRequest>
    {
        public CreateVolunteerRequestValidator()
        {
            RuleFor(c => c.fullName).MustBeValueObject(x =>
                FullName.Create(x.firstName, x.secondName, x.surname));

            RuleFor(c => c.email).MustBeValueObject(Email.Create);

            RuleFor(c => c.phone).MustBeValueObject(Phone.Create);

            RuleFor(c => c.description).MustBeValueObject(VolunteerDescription.Create);

            RuleForEach(c => c.requisiteDtos)
                .MustBeValueObject(x => Requisite.Create(x.name, x.description));

            RuleForEach(c => c.socialNetworkDtos)
                .MustBeValueObject(x => SocialNetwork.Create(x.name, x.path));
        }
    }
}
