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
                return Errors.General.ValueIsRequeired("Description");
            }

            if (description.Length > MAX_TEXT_LENGTH)
            {
                return Errors.General.ValueIsInvalid("Description");
            }

            return new PetDescription(description);
        }
    }
}
