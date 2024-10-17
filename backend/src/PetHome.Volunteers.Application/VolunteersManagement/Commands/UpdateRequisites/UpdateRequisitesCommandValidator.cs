using FluentValidation;
using PetHome.Shared.Core.Shared;
using PetHome.Shared.Core.Validation;

namespace PetHome.Volunteers.Application.VolunteersManagement.Commands.UpdateRequisites
{
    public class UpdateRequisitesCommandValidator : AbstractValidator<UpdateRequisitesCommand>
    {
        public UpdateRequisitesCommandValidator()
        {
            RuleFor(ur => ur.VolunteerId).NotEmpty()
                .WithError(Errors.General.ValueIsRequeired());

            RuleForEach(c => c.Requisites)
                .MustBeValueObject(x => Requisite.Create(x.Name, x.Description));
        }
    }
}
