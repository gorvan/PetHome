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
                return $"{nameof(HealthInfo)} "
                    + $"{nameof(health)}" + " can not be empty";
            }

            if (health.Length > Constants.MAX_TEXT_LENGTH)
            {
                return $"{nameof(HealthInfo)} "
                    + $"{nameof(health)}" + $" can not be more than " +
                    $"{Constants.MAX_TEXT_LENGTH} symbols";
            }

            var healthValue = new HealthInfo(health);

            return healthValue;
        }
    }
}
