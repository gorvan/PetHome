﻿namespace PetHome.Domain.Shared
{
    public record Error
    {
        public const string SEPARATOR = "||";
        
        public static readonly Error None
            = new(string.Empty, string.Empty, ErrorType.None);

        public string Code { get; }
        public string Message { get; }
        public ErrorType Type { get; }
        public string? InvalidField { get; } = null;

        private Error(
            string code, 
            string message, 
            ErrorType type,
            string? invalidField = null)
        {
            Code = code;
            Message = message;
            Type = type;
            InvalidField = invalidField;
        }

        public static Error Validation(
            string code, 
            string message, 
            string? InvalidField = null) =>
            new(code, message, ErrorType.Validation, InvalidField);

        public static Error NotFound(string code, string message) =>
            new(code, message, ErrorType.NotFound);

        public static Error Failure(string code, string message) =>
            new(code, message, ErrorType.Failure);

        public static Error Conflict(string code, string message) =>
            new(code, message, ErrorType.Conflict);

        public string Serialize()
        {
            return string.Join(SEPARATOR, Code, Message, Type);
        }

        public static Error Deserialize(string serialized)
        {
            var parts = serialized.Split(SEPARATOR);

            if(parts.Length < 2)
            {
                throw new ArgumentException("Invalid serialized format");
            }

            if (Enum.TryParse<ErrorType>(parts[2], out var type) == false)
            {
                throw new ArgumentException("Invalid serialized format");
            }

            return new(parts[0], parts[1], type);
        }
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
