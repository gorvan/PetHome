using PetHome.Domain.Shared;

namespace PetHome.Domain.PetManadgement.ValueObjects
{
    public record HealthInfo
    {
        private HealthInfo(string health)
        {
            Health = health;
        }

        public string Health { get; } = default!;

        public static Result<HealthInfo> Create(string health)
        {
            if (string.IsNullOrWhiteSpace(health))
            {
                return Errors.General.ValueIsRequeired("Health");
            }

            if (health.Length > Constants.MAX_TEXT_LENGTH)
            {
                return Errors.General.ValueIsInvalid("Health");
            }

            return new HealthInfo(health);
        }
    }
}
