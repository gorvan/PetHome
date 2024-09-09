using FluentValidation;

namespace PetHome.Application.Volunteers.AddPetFiles
{
    internal class AddPetFilesCommandValidator
        : AbstractValidator<AddPetFilesCommand>
    {
        public AddPetFilesCommandValidator()
        {
            RuleFor(f => f.petId).NotEmpty();
            RuleFor(f => f.VolunteerId).NotEmpty();
            RuleForEach(f => f.FilesList)
                .ChildRules(files =>
                {
                    files.RuleFor(x => x.FileName).NotEmpty();
                    files.RuleFor(x => x.ContentType).NotEmpty();
                    files.RuleFor(x => x.Stream).NotNull();
                });
        }
    }
}
