using FluentValidation;
using PetHome.Shared.Core.Shared;
using PetHome.Shared.Core.Validation;

namespace PetHome.Species.Application.SpeciesManagement.Queries.GetBreeds
{
    public class GetBreedsWithPaginationValidator :
        AbstractValidator<GetBreedsWithPaginationQuery>
    {
        public GetBreedsWithPaginationValidator()
        {
            RuleFor(b => b.SpeciesId).NotEmpty()
                .WithError(Errors.General.ValueIsRequeired());

            RuleFor(b => b.PageSize).GreaterThan(0).When(s => s.Page > 0);
        }
    }
}
