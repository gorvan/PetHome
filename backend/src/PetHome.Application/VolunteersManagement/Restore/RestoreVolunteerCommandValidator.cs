using FluentValidation;

namespace PetHome.Application.VolunteersManagement.Restore
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
