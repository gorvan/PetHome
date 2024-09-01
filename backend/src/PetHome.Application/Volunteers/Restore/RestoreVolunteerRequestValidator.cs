using FluentValidation;

namespace PetHome.Application.Volunteers.Restore
{
    public class RestoreVolunteerRequestValidator
        : AbstractValidator<RestoreVolunteerRequest>
    {
        public RestoreVolunteerRequestValidator()
        {
            RuleFor(d => d.VolunteerId).NotEmpty();
        }
    }
}
