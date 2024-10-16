using FluentValidation;
using PetHome.Shared.Core.Shared;
using PetHome.Shared.Core.Validation;

namespace PetHome.Volunteers.Application.VolunteersManagement.Commands.PetManagement.UpdatePetHelpStatus
{
    public class UpdatePetHelpStatusValidator : AbstractValidator<UpdatePetHelpStatusCommand>
    {
        public UpdatePetHelpStatusValidator()
        {
            RuleFor(v => v.VolunteerId).NotEmpty()
               .WithError(Errors.General.ValueIsRequeired());

            RuleFor(v => v.PetId).NotEmpty()
                .WithError(Errors.General.ValueIsRequeired());

            var maxStatus =
                (int)Enum.GetValues(typeof(HelpStatus)).Cast<HelpStatus>().Max();

            RuleFor(p => p.HelpStatus)
                .Must(x => (int)x >= 0 && (int)x <= maxStatus)
                .WithError(Errors.General.ValueIsInvalid());
        }
    }
}
