using PetHome.Shared.Core.Shared;

namespace PetHome.Volunteers.Domain.ValueObjects
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
