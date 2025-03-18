using FluentValidation;

namespace PetHome.VolunteerRequests.Application.VolunteerRequestManagement.Queries.GetVounteerRequestsByVolunteer;
public class GetVounteerRequestsByVolunteerQueryValidator
    : AbstractValidator<GetVounteerRequestsByVolunteerQuery>
{
    public GetVounteerRequestsByVolunteerQueryValidator()
    {
        RuleFor(c => c.VolunteerId).NotEmpty();

        RuleFor(v => v.PageSize).GreaterThan(0).When(s => s.Page > 0);
    }
}
