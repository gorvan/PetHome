using FluentValidation;

namespace PetHome.Application.VolunteersManagement.Queries.GetVolunteerById
{
    class GetVolunteerByIdValidation : AbstractValidator<GetVolunteerByIdQuery>
    {
        public GetVolunteerByIdValidation()
        {
            RuleFor(g => g.Id).NotEmpty();
        }
    }
}
