using PetHome.Domain.Shared;
using System.Text.RegularExpressions;

namespace PetHome.Domain.Models.CommonModels
{
    public record Phone
    {
        private Phone() { }

        private Phone(string phone)
        {
            PhoneNumber = phone;
        }

        public string PhoneNumber { get; } = default!;

        public static Result<Phone> Create(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone))
            {
                return $"{nameof(Phone)} " + $"{nameof(phone)}" + " can not be empty";
            }

            var regex = new Regex(Constants.PHONE_REGULAR_EXPR);
            if (!regex.IsMatch(phone))
            {
                return $"{nameof(Phone)} has incorrect format";
            }

            var phoneValue = new Phone(phone);

            return phoneValue;
        }
    }
}
