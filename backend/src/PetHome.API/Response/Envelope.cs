using PetHome.Domain.Shared;

namespace PetHome.API.Response
{
    public record ResponseError(
        string? ErrorCode,
        string? ErrorMessage,
        string InvalidField);

    public record Envelope
    {
        public object? Result { get; }
        public List<ResponseError> ResponceErrors { get; }
        public List<Error> Errors { get; }
        public DateTime TimeGenerated { get; }

        private Envelope(
            object? result,
            IEnumerable<ResponseError> errors)
        {
            Result = result;
            ResponceErrors = errors.ToList();
            TimeGenerated = DateTime.Now;
            Errors = [];
        }

        private Envelope(
            object? result,
            IEnumerable<Error> errors)
        {
            Result = result;
            Errors = errors.ToList();
            TimeGenerated = DateTime.Now;
            ResponceErrors= [];
        }

        public static Envelope Ok(object? result = null) =>
            new(result, new List<ResponseError>());

        public static Envelope Error(IEnumerable<ResponseError> errors) =>
            new(null, errors);
        public static Envelope Error(IEnumerable<Error> errors) =>
            new(null, errors);
    }
}
