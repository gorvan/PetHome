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
                return Errors.General.ValueIsRequeired("Color");
            }

            return new PetColor(color);
        }
    }
}
