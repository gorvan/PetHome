using FluentValidation;
using PetHome.Shared.Core.Shared;
using PetHome.Shared.Core.Validation;

namespace PetHome.Volunteers.Application.VolunteersManagement.Commands.PetManagement.DeletePet
{
    public class DeletePetCommandValidator : AbstractValidator<DeletePetCommand>
    {
        public DeletePetCommandValidator()
        {
            RuleFor(v => v.VolunteerId).NotEmpty()
                .WithError(Errors.General.ValueIsRequeired());

            RuleFor(v => v.PetId).NotEmpty()
                .WithError(Errors.General.ValueIsRequeired());
        }
    }
}
