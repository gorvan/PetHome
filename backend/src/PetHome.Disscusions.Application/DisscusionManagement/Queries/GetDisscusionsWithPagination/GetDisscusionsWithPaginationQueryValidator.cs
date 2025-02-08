using FluentValidation;
using PetHome.Shared.Core.Shared;
using PetHome.Shared.Core.Validation;

namespace PetHome.Disscusions.Application.DisscusionManagement.Queries.GetDisscusionsWithPagination;
public class GetDisscusionsWithPaginationQueryValidator : AbstractValidator<GetDisscusionsWithPaginationQuery>
{
    public GetDisscusionsWithPaginationQueryValidator()
    {
        RuleFor(d => d.Page).GreaterThan(0)
               .WithError(Errors.General.ValueIsRequeired());

        RuleFor(d => d.PageSize).GreaterThan(0)
               .WithError(Errors.General.ValueIsRequeired());
    }
}
