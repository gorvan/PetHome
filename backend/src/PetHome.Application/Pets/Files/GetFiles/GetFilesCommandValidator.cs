using FluentValidation;

namespace PetHome.Application.Pets.Files.GetFiles
{
    public class GetFilesCommandValidator : AbstractValidator<GetFilesCommand>
    {
        public GetFilesCommandValidator()
        {
            RuleFor(d => d.BucketName).NotEmpty();
            RuleFor(d => d.FilePrefix).NotEmpty();
        }
    }
}
