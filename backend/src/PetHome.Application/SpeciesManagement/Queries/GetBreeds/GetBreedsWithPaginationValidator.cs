using FluentValidation;
using PetHome.Application.Validation;
using PetHome.Domain.Shared;

namespace PetHome.Application.SpeciesManagement.Queries.GetBreeds
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
