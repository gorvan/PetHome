using PetHome.Shared.Core.Shared;

namespace PetHome.Volunteers.Domain.ValueObjects
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
                return Errors.General.ValueIsRequeired("Description");
            }

            if (description.Length > MAX_TEXT_LENGTH)
            {
                return Errors.General.ValueIsInvalid("Description");
            }

            return new VolunteerDescription(description);
        }
    }
}
