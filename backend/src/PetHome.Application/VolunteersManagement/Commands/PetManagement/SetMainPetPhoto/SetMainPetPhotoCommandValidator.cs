using FluentValidation;
using PetHome.Application.Validation;
using PetHome.Domain.Shared;

namespace PetHome.Application.VolunteersManagement.Commands.PetManagement.SetMainPetPhoto
{
    public class SetMainPetPhotoCommandValidator : AbstractValidator<SetMainPetPhotoCommand>
    {
        public SetMainPetPhotoCommandValidator()
        {
            RuleFor(p => p.VolunteerId).NotEmpty()
               .WithError(Errors.General.ValueIsRequeired());

            RuleFor(p => p.PetId).NotEmpty()
                .WithError(Errors.General.ValueIsRequeired());

            RuleFor(p => p.FilePath).NotEmpty()
                .WithError(Errors.General.ValueIsRequeired());
        }
    }
}
