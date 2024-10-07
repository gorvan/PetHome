using FluentValidation;
using PetHome.Application.Validation;
using PetHome.Domain.Shared;

namespace PetHome.Application.VolunteersManagement.Commands.PetManagement.DeletePet
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
