namespace PetHome.Shared.Core.Shared
{
    public record Description
    {
        public const int MAX_TEXT_LENGTH = 2000;

        private Description(string description)
        {
            DescriptionValue = description;
        }


        public string DescriptionValue { get; } = default!;


        public static Result<Description> Create(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
            {
                return Errors.General.ValueIsRequeired("Description");
            }

            if (description.Length > MAX_TEXT_LENGTH)
            {
                return Errors.General.ValueIsInvalid("Description");
            }

            return new Description(description);
        }
    }
}
