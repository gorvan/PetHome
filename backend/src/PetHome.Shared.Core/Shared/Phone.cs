using System.Text.RegularExpressions;

namespace PetHome.Shared.Core.Shared
{
    public record Phone
    {
        public const string PHONE_REGULAR_EXPR =
            @"^(\+)?((\d{2,3}) ?\d|\d)(([ -]?\d)|( ?(\d{2,3}) ?)){5,12}\d$";

        private Phone(string phoneNumber)
        {
            PhoneNumber = phoneNumber;
        }

        public string PhoneNumber { get; } = default!;

        public static Result<Phone> Create(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone))
            {
                return Errors.General.ValueIsRequeired("PhoneNumber");
            }

            var regex = new Regex(PHONE_REGULAR_EXPR);
            if (!regex.IsMatch(phone))
            {
                return Errors.General.ValueIsInvalid("PhoneNumber");
            }

            return new Phone(phone);
        }
    }
}
