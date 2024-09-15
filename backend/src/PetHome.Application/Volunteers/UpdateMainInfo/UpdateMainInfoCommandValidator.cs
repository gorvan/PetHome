using FluentValidation;
using PetHome.Application.Validation;
using PetHome.Domain.Shared;

namespace PetHome.Application.Volunteers.UpdateMainInfo
{
    public class UpdateMainInfoCommandValidator
        : AbstractValidator<UpdateMainInfoCommand>
    {
        public UpdateMainInfoCommandValidator()
        {
            RuleFor(v => v.VolunteerId).NotEmpty()
                .WithError(Errors.General.ValueIsRequeired());
        }
    }
}
