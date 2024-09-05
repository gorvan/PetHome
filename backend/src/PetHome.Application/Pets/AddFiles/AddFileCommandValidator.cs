using FluentValidation;

namespace PetHome.Application.Pets.AddFiles
{
    public class AddFileCommandValidator : AbstractValidator<AddFileCommand>
    {
        public AddFileCommandValidator()
        {
            RuleFor(d => d.BucketName).NotEmpty();
            RuleFor(d => d.FilePath).NotEmpty();
        }
    }
}
