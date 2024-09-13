using FluentValidation;

namespace PetHome.Application.Volunteers.Restore
{
    public class RestoreVolunteerCommandValidator
        : AbstractValidator<RestoreVolunteerCommand>
    {
        public RestoreVolunteerCommandValidator()
        {
            RuleFor(d => d.VolunteerId).NotEmpty();
        }
    }
}
