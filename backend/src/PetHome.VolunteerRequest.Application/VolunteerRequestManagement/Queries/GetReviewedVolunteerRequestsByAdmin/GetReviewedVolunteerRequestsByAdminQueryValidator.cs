using FluentValidation;

namespace PetHome.VolunteerRequests.Application.VolunteerRequestManagement.Queries.GetReviewedVolunteerRequestsByAdmin;
public class GetReviewedVolunteerRequestsByAdminQueryValidator
    : AbstractValidator<GetReviewedVolunteerRequestsByAdminQuery>
{
    public GetReviewedVolunteerRequestsByAdminQueryValidator()
    {
        RuleFor(c => c.AdminId).NotEmpty();

        RuleFor(v => v.PageSize).GreaterThan(0).When(s => s.Page > 0);
    }
}
