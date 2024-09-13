using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using PetHome.API.Response;
using PetHome.Domain.Shared;


namespace PetHome.API.Extensions
{
    public static class ResponseExtensions
    {
        public static ActionResult ToResponse(this Error error)
        {
            var statusCode = GetStatusCode(error.Type);

            var responseError = new ResponseError(error.Code, error.Message, string.Empty);

            var envelope = Envelope.Error([responseError]);

            return new ObjectResult(envelope)
            {
                StatusCode = statusCode
            };
        }

        public static ActionResult ToResponse(this Result result)
        {
            if (result.IsSuccess)
            {
                return new OkObjectResult(Envelope.Ok(result));
            }
            var statusCode = GetStatusCode(result.Error.Type);

            var responseError = new ResponseError(result.Error.Code, result.Error.Message, string.Empty);

            var envelope = Envelope.Error([responseError]);

            return new ObjectResult(envelope)
            {
                StatusCode = statusCode
            };
        }

        public static ActionResult<T> ToResponse<T>(this Result<T> result)
        {
            if (result.IsSuccess)
            {
                return new OkObjectResult(Envelope.Ok(result.Value));
            }

            var statusCode = GetStatusCode(result.Error.Type);

            var responseError = new ResponseError(result.Error.Code, result.Error.Message, string.Empty);

            var envelope = Envelope.Error([responseError]);


            return new ObjectResult(envelope)
            {
                StatusCode = statusCode
            };
        }

        public static ActionResult ToErrorValidationResponse(this ValidationResult result)
        {
            if (result.IsValid)
                throw new InvalidOperationException("Result can not be succeed");

            var validationErrors = result.Errors;

            var responseErrors = from validationError in validationErrors
                                 let errorMessage = validationError.ErrorMessage
                                 let error = Error.Deserialize(errorMessage)
                                 select new ResponseError(error.Code, error.Message, validationError.PropertyName);

            var envelope = Envelope.Error(responseErrors);

            return new ObjectResult(envelope)
            {
                StatusCode = StatusCodes.Status400BadRequest
            };
        }

        private static int GetStatusCode(ErrorType errorType)
        {
            return errorType switch
            {
                ErrorType.Validation => StatusCodes.Status400BadRequest,
                ErrorType.NotFound => StatusCodes.Status404NotFound,
                ErrorType.Conflict => StatusCodes.Status409Conflict,
                ErrorType.Failure => StatusCodes.Status500InternalServerError,
                _ => StatusCodes.Status500InternalServerError
            };
        }
    }
}
