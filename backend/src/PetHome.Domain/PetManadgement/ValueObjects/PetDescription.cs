using PetHome.Domain.Shared;

namespace PetHome.Domain.PetManadgement.ValueObjects
{
    public record PetDescription
    {
        public const int MAX_TEXT_LENGTH = 2000;

        private PetDescription(string description)
        {
            Description = description;
        }


        public string Description { get; } = default!;


        public static Result<PetDescription> Create(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
            {
                return $"{nameof(PetDescription)} "
                    + $"{nameof(description)}" + " can not be empty";
            }

            if (description.Length > MAX_TEXT_LENGTH)
            {
                return $"{nameof(PetDescription)} "
                    + $"{nameof(description)}" + $" can not be more than " +
                    $"{MAX_TEXT_LENGTH} symbols";
            }

            var descriptionValue = new PetDescription(description);

            return descriptionValue;
        }
    }
}
