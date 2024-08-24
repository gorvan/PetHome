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

            var envelope = Envelope.Error(error);

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

            var envelope = Envelope.Error(result.Error);

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

            var envelope = Envelope.Error(result.Error);

            return new ObjectResult(envelope)
            {
                StatusCode = statusCode
            };
        }

        private static int GetStatusCode(ErrorType errorType)
        {
            return errorType switch
            {
                ErrorType.Validtion => StatusCodes.Status400BadRequest,
                ErrorType.NotFound => StatusCodes.Status404NotFound,
                ErrorType.Conflict => StatusCodes.Status409Conflict,
                ErrorType.Failure => StatusCodes.Status500InternalServerError,
                _ => StatusCodes.Status500InternalServerError
            };
        }
    }
}
