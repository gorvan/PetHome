using FluentValidation;
using PetHome.Application.Validation;
using PetHome.Domain.Shared;

namespace PetHome.Application.SpeciesManagement.Commands.Delete
{
    public class DeleteSpeciesValidator : AbstractValidator<DeleteSpeciesCommand>
    {
        public DeleteSpeciesValidator()
        {
            RuleFor(d => d.SpeciesId).NotEmpty()
               .WithError(Errors.General.ValueIsRequeired());
        }
    }
}
