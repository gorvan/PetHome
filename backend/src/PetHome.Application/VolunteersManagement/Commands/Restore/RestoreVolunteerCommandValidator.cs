using FluentValidation;

namespace PetHome.Application.VolunteersManagement.Commands.Restore
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
