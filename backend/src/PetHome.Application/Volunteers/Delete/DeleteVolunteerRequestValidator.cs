using FluentValidation;

namespace PetHome.Application.Volunteers.Delete
{
    internal class DeleteVolunteerRequestValidator
        : AbstractValidator<DeleteVolunteerRequest>
    {
        public DeleteVolunteerRequestValidator()
        {
            RuleFor(d => d.VolunteerId).NotEmpty();
        }
    }
}
