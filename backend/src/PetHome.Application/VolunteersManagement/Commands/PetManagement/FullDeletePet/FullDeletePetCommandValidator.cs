using FluentValidation;
using PetHome.Application.Validation;
using PetHome.Domain.Shared;

namespace PetHome.Application.VolunteersManagement.Commands.PetManagement.FullDeletePet
{
    public class FullDeletePetCommandValidator : AbstractValidator<FullDeletePetCommand>
    {
        public FullDeletePetCommandValidator()
        {
            RuleFor(v => v.VolunteerId).NotEmpty()
                .WithError(Errors.General.ValueIsRequeired());

            RuleFor(v => v.PetId).NotEmpty()
                .WithError(Errors.General.ValueIsRequeired());
        }
    }
}
