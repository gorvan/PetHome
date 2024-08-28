using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PetHome.API.Response;
using PetHome.Domain.Shared;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Results;

namespace PetHome.API.Validation
{
    public class CustomResultFactory : IFluentValidationAutoValidationResultFactory
    {
        public IActionResult CreateActionResult(
            ActionExecutingContext context,
            ValidationProblemDetails? validationProblemDetails)
        {
            if (validationProblemDetails is null)
            {
                throw new InvalidOperationException("ValidationProblemDetails is null");
            }

            List<ResponseError> errors = [];

            foreach (var (invalidField, validationErrors) in validationProblemDetails.Errors)
            {
                var responseErrors = from errorMessage in validationErrors
                                     let error = Error.Deserialize(errorMessage)
                                     let responseError = new ResponseError(
                                             error.Code,
                                             error.Message,
                                             invalidField)
                                     select responseError;

                errors.AddRange(responseErrors);
            }

            var envelope = Envelope.Error(errors);

            return new ObjectResult(envelope)
            {
                StatusCode = StatusCodes.Status400BadRequest
            };
        }
    }
}
