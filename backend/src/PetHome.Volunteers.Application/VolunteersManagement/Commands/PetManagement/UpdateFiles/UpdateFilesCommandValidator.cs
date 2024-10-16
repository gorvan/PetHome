using FluentValidation;
using PetHome.Shared.Core.Shared;
using PetHome.Shared.Core.Validation;
using PetHome.Volunteers.Application.VolunteersManagement.Commands.PetManagement.AddPetFiles;

namespace PetHome.Volunteers.Application.VolunteersManagement.Commands.PetManagement.UpdateFiles
{
    public class UpdateFilesCommandValidator : AbstractValidator<UpdateFilesCommand>
    {
        public UpdateFilesCommandValidator()
        {
            RuleFor(v => v.VolunteerId).NotEmpty()
               .WithError(Errors.General.ValueIsRequeired());

            RuleFor(v => v.petId).NotEmpty()
                .WithError(Errors.General.ValueIsRequeired());

            RuleForEach(f => f.FilesList)
                .ChildRules(files =>
                {
                    files.RuleFor(x => x.FileName).NotEmpty()
                    .WithError(Errors.General.ValueIsRequeired());

                    files.RuleFor(x => x.ContentType).NotEmpty()
                        .Must(c => AddPetFilesCommandValidator.PERMITED_EXTENSIONS.Any(x => x == c))
                        .WithError(Errors.General.ValueIsInvalid());

                    files.RuleFor(x => x.Stream).NotNull()
                        .Must(s => s.Length < AddPetFilesCommandValidator.MAX_FILE_SIZE)
                        .WithError(Errors.General.ValueIsInvalid());
                });
        }
    }
}
