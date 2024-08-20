using PetHome.Domain.Shared;

namespace PetHome.Domain.Models.CommonModels
{
    public record NotNullableString
    {
        private NotNullableString(string value)
        {
            Value = value;
        }

        public string Value { get; } = default!;

        public static Result<NotNullableString> Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return "Value can not be empty";
            }

            if (value.Length > Constants.MAX_TEXT_LENGTH)
            {
                return $"Value can not be more than {Constants.MAX_TITLE_LENGTH} symbols";
            }

            var valueObject = new NotNullableString(value);
            return valueObject;
        }

        public static implicit operator string(NotNullableString value)
        {
            return value.Value;
        }
    }
}
