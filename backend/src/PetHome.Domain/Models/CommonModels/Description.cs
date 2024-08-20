using PetHome.Domain.Shared;

namespace PetHome.Domain.Models.CommonModels
{
    public record Description
    {
        private Description(string description)
        {
            Value = description;
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
    }
}
