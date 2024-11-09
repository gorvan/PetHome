namespace PetHome.Shared.Core.Shared
{
    public record DescriptionValueObject
    {
        public const int MAX_TEXT_LENGTH = 2000;

        private DescriptionValueObject(string description)
        {
            Description = description;
        }

        public string Description { get; } = default!;


        public static Result<DescriptionValueObject> Create(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
            {
                return Errors.General.ValueIsRequeired("Description");
            }

            if (description.Length > MAX_TEXT_LENGTH)
            {
                return Errors.General.ValueIsInvalid("Description");
            }

            return new DescriptionValueObject(description);
        }
    }
}
