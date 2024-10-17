using FluentValidation;

namespace PetHome.Species.Application.SpeciesManagement.Queries.GetSpecies
{
    public class GetSpeciesWithPaginationValidator : AbstractValidator<GetSpeciesWithPaginationQuery>
    {
        public GetSpeciesWithPaginationValidator()
        {
            RuleFor(s => s.PageSize).GreaterThan(0).When(s => s.Page > 0);
        }
    }
}
