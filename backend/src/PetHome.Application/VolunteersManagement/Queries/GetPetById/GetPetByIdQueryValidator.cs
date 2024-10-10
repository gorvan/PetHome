using FluentValidation;

namespace PetHome.Application.VolunteersManagement.Queries.GetPetById
{
    public class GetPetByIdQueryValidator : AbstractValidator<GetPetByIdQuery>
    {
        public GetPetByIdQueryValidator()
        {
            RuleFor(g => g.petId).NotEmpty();
        }
    }
}
