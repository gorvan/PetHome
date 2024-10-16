using FluentValidation;

namespace PetHome.Volunteers.Application.VolunteersManagement.Commands.Restore
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
