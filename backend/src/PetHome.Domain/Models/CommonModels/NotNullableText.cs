using PetHome.Domain.Shared;

namespace PetHome.Domain.Models.CommonModels
{
    public record NotNullableText
    {
        private NotNullableText(string text)
        {
            Text = text;
        }

        public string Text { get; } = default!;

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
