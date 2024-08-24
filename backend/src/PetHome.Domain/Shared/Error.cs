﻿namespace PetHome.Domain.Shared
{
    public record Error
    {
        public static readonly Error None
            = new Error(string.Empty, string.Empty, ErrorType.None);

        public string Code { get; }
        public string Message { get; }
        public ErrorType Type { get; }

        private Error(string code, string message, ErrorType type)
        {
            Code = code;
            Message = message;
            Type = type;
        }

        public static Error Validation(string code, string message) =>
            new Error(code, message, ErrorType.Validation);

        public static Error NotFound(string code, string message) =>
            new Error(code, message, ErrorType.NotFound);

        public static Error Failure(string code, string message) =>
            new Error(code, message, ErrorType.Failure);

        public static Error Conflict(string code, string message) =>
            new Error(code, message, ErrorType.Conflict);
    }

    public enum ErrorType
    {
        None,
        Validation,
        NotFound,
        Failure,
        Conflict
    }
}
