using FluentValidation;

namespace PetHome.Application.Pets.GetFile
{
    public class GetFileCommandValidator : AbstractValidator<GetFileCommand>
    {
        public GetFileCommandValidator()
        {
            RuleFor(d => d.BucketName).NotEmpty();
            RuleFor(d => d.FilePath).NotEmpty();
        }
    }
}
