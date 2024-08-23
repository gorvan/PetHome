using PetHome.Domain.Shared;

namespace PetHome.Domain.PetManadgement.ValueObjects
{
    public record PetColor
    {
        private PetColor(string color)
        {
            Color = color;
        }

        public string Color { get; } = default!;

        public static Result<PetColor> Create(string color)
        {
            if (string.IsNullOrWhiteSpace(color))
            {
                return $"{nameof(PetColor)} "
                    + $"{nameof(color)}" + " can not be empty";
            }

            var colorValue = new PetColor(color);

            return colorValue;
        }
    }
}
