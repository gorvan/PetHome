using FluentValidation;
using PetHome.Shared.Core.Shared;
using PetHome.Shared.Core.Validation;

namespace PetHome.Volunteers.Application.VolunteersManagement.Commands.PetManagement.FullDeletePet
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
