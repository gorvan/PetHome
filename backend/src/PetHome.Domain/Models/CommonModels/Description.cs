using PetHome.Domain.Shared;

namespace PetHome.Domain.Models.CommonModels
{
    public record Description
    {
        private Description(string value)
        {
            Value = value;
        }        

        public string Value { get; } = default!;


        public static Result<Description> Create(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
            {
                return $"{nameof(Description)} " + $"{nameof(description)}" + " can not be empty";
            }

            if (description.Length > Constants.MAX_TEXT_LENGTH)
            {
                return $"{nameof(Description)} " + $"{nameof(description)}" + $" can not be more than {Constants.MAX_TEXT_LENGTH} symbols";
            }

            var descriptionValue = new Description(description);

            return descriptionValue;
        }

        public static implicit operator string(Description value)
        {
            return value.Value;
        }
    }
}
