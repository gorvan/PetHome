﻿using FluentValidation;

namespace PetHome.Application.Pets.Delete
{
    public class DeleteFileCommandValidator : AbstractValidator<DeleteFileCommand>
    {
        public DeleteFileCommandValidator()
        {
            RuleFor(d => d.BucketName).NotEmpty();
            RuleFor(d => d.FilePath).NotEmpty();
        }
    }
}