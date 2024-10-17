using FluentValidation;
using PetHome.Shared.Core.Shared;
using PetHome.Shared.Core.Validation;

namespace PetHome.Species.Application.SpeciesManagement.Commands.Delete
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
