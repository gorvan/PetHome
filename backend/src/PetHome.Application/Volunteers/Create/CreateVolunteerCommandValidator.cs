using FluentValidation;
using PetHome.Application.Validation;
using PetHome.Domain.PetManadgement.ValueObjects;
using PetHome.Domain.Shared;

namespace PetHome.Application.Volunteers.Create
{
    public class CreateVolunteerCommandValidator : AbstractValidator<CreateVolunteerCommand>
    {
        public CreateVolunteerCommandValidator()
        {
            RuleFor(c => c.fullName).MustBeValueObject(x =>
                FullName.Create(x.FirstName, x.SecondName, x.Surname));

            RuleFor(c => c.email).MustBeValueObject(Email.Create);

            RuleFor(c => c.phone).MustBeValueObject(Phone.Create);

            RuleFor(c => c.description).MustBeValueObject(VolunteerDescription.Create);

            RuleForEach(c => c.requisiteDtos)
                .MustBeValueObject(x => Requisite.Create(x.Name, x.Description));

            RuleForEach(c => c.socialNetworkDtos)
                .MustBeValueObject(x => SocialNetwork.Create(x.Name, x.Path));
        }
    }
}
