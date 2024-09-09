using FluentValidation;
using PetHome.Domain.Shared;

namespace PetHome.Application.Volunteers.AddPet
{
    public class AddPetCommandValidator : AbstractValidator<AddPetCommand>
    {
        public AddPetCommandValidator()
        {
            RuleFor(p => p.VolunteerId).NotEmpty();
            RuleFor(p => p.Nickname).NotEmpty();
            RuleFor(p => p.Description).NotEmpty();
            RuleFor(p => p.Color).NotEmpty();
            RuleFor(p => p.Health).NotEmpty();
            RuleFor(p => p.Address.City).NotEmpty();
            RuleFor(p => p.Address.Street).NotEmpty();
            RuleFor(p => p.Address.HouseNumber).NotEmpty();
            RuleFor(p => p.Address.AppartmentNumber).NotEmpty();
            RuleFor(p => p.Phone).NotEmpty();
            RuleFor(p => p.Requisites.Name).NotEmpty();
            RuleFor(p => p.Requisites.Description).NotEmpty();
            RuleFor(p => p.BirthDay).NotEmpty();

            var maxStatus =
                (int)Enum.GetValues(typeof(HelpStatus)).Cast<HelpStatus>().Max();

            RuleFor(p => p.HelpStatus)
                .Must(x => (int)x >= 0 && (int)x <= maxStatus);

            RuleFor(p => p.Height).GreaterThan(0);
            RuleFor(p => p.Weight).GreaterThan(0);
        }
    }
}
