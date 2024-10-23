using FluentValidation.Results;
using PetHome.Shared.Core.Shared;

namespace PetHome.Shared.Core.Extensions
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

        public static List<Error> ToErrorList(this IEnumerable<Error> errors)
        {
            return (from error in errors                   
                    let errorResponse = Error.Validation(
                                error.Code,
                                error.Message,
                                error.Type.ToString())
                    select errorResponse).ToList();
        }
    }
}
