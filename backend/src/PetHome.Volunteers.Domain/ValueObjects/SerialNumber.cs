using PetHome.Shared.Core.Shared;

namespace PetHome.Volunteers.Domain.ValueObjects
{
    public record SerialNumber
    {
        private SerialNumber(int value)
        {
            Value = value;
        }

        public int Value { get; }
        public static Result<SerialNumber> Create(int number)
        {
            if (number < 1)
            {
                return Errors.General.ValueIsInvalid("serial number");
            }

            return new SerialNumber(number);
        }
    }
}
