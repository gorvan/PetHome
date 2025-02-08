using FluentValidation;
using PetHome.Shared.Core.Shared;
using PetHome.Shared.Core.Validation;

namespace PetHome.Disscusions.Application.DisscusionManagement.Queries.GetDisscusionByRealtionId;
public class GetDisscusionByRelationIdQueryValidator : AbstractValidator<GetDisscusionByRelationIdQuery>
{
    public GetDisscusionByRelationIdQueryValidator()
    {
        RuleFor(d => d.RelationId).NotEmpty()
               .WithError(Errors.General.ValueIsRequeired());
    }
}
