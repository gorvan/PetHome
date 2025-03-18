using FluentValidation;

namespace PetHome.VolunteerRequests.Application.VolunteerRequestManagement.Queries.GetNotReviewedVolunteerRequests;
public class GetNotReviewedVolunteerRequestsQueryValidator 
    : AbstractValidator<GetNotReviewedVolunteerRequestsQuery>
{
    public GetNotReviewedVolunteerRequestsQueryValidator()
    {
        RuleFor(v => v.AdminId).NotEmpty();

        RuleFor(v => v.PageSize).GreaterThan(0).When(s => s.Page > 0);
    }
}
