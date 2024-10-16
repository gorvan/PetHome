using FluentValidation;
using PetHome.Shared.Core.Shared;
using PetHome.Shared.Core.Validation;

namespace PetHome.Volunteers.Application.VolunteersManagement.Commands.Delete
{
    public class DeleteVolunteerCommandValidator
        : AbstractValidator<DeleteVolunteerCommand>
    {
        public DeleteVolunteerCommandValidator()
        {
            RuleFor(d => d.VolunteerId).NotEmpty()
                .WithError(Errors.General.ValueIsRequeired());
        }
    }
}
