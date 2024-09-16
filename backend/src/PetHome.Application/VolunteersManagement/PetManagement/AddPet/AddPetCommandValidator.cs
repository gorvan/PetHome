using FluentValidation;
using PetHome.Application.Validation;
using PetHome.Domain.PetManadgement.ValueObjects;
using PetHome.Domain.Shared;

namespace PetHome.Application.VolunteersManagement.PetManagement.AddPet
{
    public class AddPetCommandValidator : AbstractValidator<AddPetCommand>
    {
        public AddPetCommandValidator()
        {
            RuleFor(v => v.VolunteerId).NotEmpty()
                .WithError(Errors.General.ValueIsRequeired());

            RuleFor(c => c.Nickname).MustBeValueObject(
                PetNickname.Create);

            RuleFor(c => c.Description).MustBeValueObject(
                PetDescription.Create);

            RuleFor(c => c.Color).MustBeValueObject(
                PetColor.Create);

            RuleFor(p => p.Health).MustBeValueObject(
                HealthInfo.Create);

            RuleFor(c => c.Address).MustBeValueObject(x =>
                Address.Create(x.City, x.Street, x.HouseNumber, x.AppartmentNumber));

            RuleFor(p => p.Phone).MustBeValueObject(
                Phone.Create);

            RuleFor(p => p.Requisites).MustBeValueObject(x =>
                 Requisite.Create(x.Name, x.Description));

            RuleFor(p => p.BirthDay).MustBeValueObject(
                DateValue.Create);

            var maxStatus =
                (int)Enum.GetValues(typeof(HelpStatus)).Cast<HelpStatus>().Max();

            RuleFor(p => p.HelpStatus)
                .Must(x => (int)x >= 0 && (int)x <= maxStatus)
                .WithError(Errors.General.ValueIsInvalid());

            RuleFor(p => p.Height).GreaterThan(0)
            .WithError(Errors.General.ValueIsInvalid());

            RuleFor(p => p.Weight).GreaterThan(0)
                .WithError(Errors.General.ValueIsInvalid());
        }
    }
}
