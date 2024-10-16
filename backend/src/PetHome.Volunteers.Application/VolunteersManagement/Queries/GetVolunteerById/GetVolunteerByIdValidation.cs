using FluentValidation;

namespace PetHome.Volunteers.Application.VolunteersManagement.Queries.GetVolunteerById
{
    class GetVolunteerByIdValidation : AbstractValidator<GetVolunteerByIdQuery>
    {
        public GetVolunteerByIdValidation()
        {
            RuleFor(g => g.Id).NotEmpty();
        }
    }
}
