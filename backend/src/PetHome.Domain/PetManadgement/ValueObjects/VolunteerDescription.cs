using PetHome.Domain.Shared;

namespace PetHome.Domain.PetManadgement.ValueObjects
{
    public record VolunteerDescription
    {
        public const int MAX_TEXT_LENGTH = 2000;

        private VolunteerDescription(string description)
        {
            Description = description;
        }


        public string Description { get; } = default!;


        public static Result<VolunteerDescription> Create(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
            {
                return $"{nameof(VolunteerDescription)} "
                    + $"{nameof(description)}" + " can not be empty";
            }

            if (description.Length > MAX_TEXT_LENGTH)
            {
                return $"{nameof(VolunteerDescription)} "
                    + $"{nameof(description)}" + $" can not be more than " +
                    $"{MAX_TEXT_LENGTH} symbols";
            }

            var descriptionValue = new VolunteerDescription(description);

            return descriptionValue;
        }
    }
}
