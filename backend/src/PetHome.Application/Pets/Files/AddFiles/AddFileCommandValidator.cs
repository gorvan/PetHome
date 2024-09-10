using FluentValidation;

namespace PetHome.Application.Pets.Files.AddFiles
{
    public class AddFileCommandValidator : AbstractValidator<AddFileCommand>
    {
        public AddFileCommandValidator()
        {
            RuleFor(d => d.FileStream).NotEmpty();
            RuleFor(d => d.FilePath).NotEmpty();
        }
    }
}
