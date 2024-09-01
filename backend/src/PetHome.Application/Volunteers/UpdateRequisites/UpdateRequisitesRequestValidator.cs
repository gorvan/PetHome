using FluentValidation;
using PetHome.Application.Validation;
using PetHome.Domain.Shared;

namespace PetHome.Application.Volunteers.UpdateRequisites
{
    public class UpdateRequisitesRequestValidator : AbstractValidator<UpdateRequisitesRequest>
    {
        public UpdateRequisitesRequestValidator()
        {
            RuleFor(ur => ur.VolunteerId).NotEmpty()
                .WithError(Errors.General.ValueIsRequeired());

            RuleForEach(c => c.Requisites)
                .MustBeValueObject(x => Requisite.Create(x.Name, x.Description));
        }
    }
}
