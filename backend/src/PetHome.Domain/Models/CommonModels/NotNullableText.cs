using PetHome.Domain.Shared;

namespace PetHome.Domain.Models.CommonModels
{
    public record NotNullableText
    {
        private NotNullableText(string value)
        {
            this.Value = value;
        }

        public string Value { get; } = default!;

        public static Result<NotNullableText> Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return "Value can not be empty";
            }

            if (value.Length > Constants.MAX_TEXT_LENGTH)
            {
                return $"Value can not be more than {Constants.MAX_TEXT_LENGTH} symbols";
            }

            var valueObject = new NotNullableText(value);
            return valueObject;
        }

    }
}
