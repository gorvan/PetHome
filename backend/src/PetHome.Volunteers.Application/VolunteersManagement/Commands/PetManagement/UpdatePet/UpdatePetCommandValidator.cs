using FluentValidation;
using PetHome.Shared.Core.Shared;
using PetHome.Shared.Core.Validation;
using PetHome.Volunteers.Domain.ValueObjects;


namespace PetHome.Volunteers.Application.VolunteersManagement.Commands.PetManagement.UpdatePet
{
    public class UpdatePetCommandValidator : AbstractValidator<UpdatePetCommand>
    {
        public UpdatePetCommandValidator()
        {
            RuleFor(v => v.VolunteerId).NotEmpty()
                .WithError(Errors.General.ValueIsRequeired());

            RuleFor(v => v.PetId).NotEmpty()
                .WithError(Errors.General.ValueIsRequeired());

            RuleFor(p => p.Nickname).MustBeValueObject(
                PetNickname.Create);

            RuleFor(v => v.SpeciesId).NotEmpty()
                .WithError(Errors.General.ValueIsRequeired());

            RuleFor(v => v.BreedId).NotEmpty()
                .WithError(Errors.General.ValueIsRequeired());

            RuleFor(p => p.Description).MustBeValueObject(
                Description.Create);

            RuleFor(p => p.Color).MustBeValueObject(
                PetColor.Create);

            RuleFor(p => p.Health).MustBeValueObject(
                HealthInfo.Create);

            RuleFor(p => p.Address).MustBeValueObject(x =>
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
