using FluentValidation.Results;
using PetHome.Domain.Shared;

namespace PetHome.Application.Validation
{
    public static class ValidationExtension
    {
        public static List<Error> ToErrorList(this ValidationResult result)
        {
            if (result.IsValid)
                throw new InvalidOperationException("Result can not be succeed");

            return (from error in result.Errors
                    let deserializedError = Error.Deserialize(error.ErrorMessage)
                    let errorResponse = Error.Validation(
                                deserializedError.Code,
                                error.ErrorMessage,
                                error.PropertyName)
                    select errorResponse).ToList();
        }
    }
}
