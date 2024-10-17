using FluentValidation;

namespace PetHome.Volunteers.Application.VolunteersManagement.Queries.GetPetById
{
    public class GetPetByIdQueryValidator : AbstractValidator<GetPetByIdQuery>
    {
        public GetPetByIdQueryValidator()
        {
            RuleFor(g => g.petId).NotEmpty();
        }
    }
}
