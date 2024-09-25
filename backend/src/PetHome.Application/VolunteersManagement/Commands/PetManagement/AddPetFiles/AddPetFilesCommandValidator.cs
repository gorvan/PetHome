using FluentValidation;
using PetHome.Application.Validation;
using PetHome.Domain.Shared;

namespace PetHome.Application.VolunteersManagement.Commands.PetManagement.AddPetFiles
{
    public class AddPetFilesCommandValidator
        : AbstractValidator<AddPetFilesCommand>
    {
        public static string[] PERMITED_EXTENSIONS =
            { "image/jpg", "image/jpeg", "image/png", "image/gif" };

        public static long MAX_FILE_SIZE = 5242880;

        public AddPetFilesCommandValidator()
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
                        .Must(c => PERMITED_EXTENSIONS.Any(x => x == c))
                        .WithError(Errors.General.ValueIsInvalid());

                    files.RuleFor(x => x.Stream).NotNull()
                        .Must(s => s.Length < MAX_FILE_SIZE)
                        .WithError(Errors.General.ValueIsInvalid());
                });
        }
    }
}
