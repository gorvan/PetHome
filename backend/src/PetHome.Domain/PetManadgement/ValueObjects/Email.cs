using PetHome.Domain.Shared;
using System.Text.RegularExpressions;

namespace PetHome.Domain.PetManadgement.ValueObjects
{
    public record Email
    {
        public const string EMAIL_REGULAR_EXPR
            = @"^[-\w.]+@([A-z0-9][-A-z0-9]+\.)+[A-z]{2,4}$";

        private Email(string emailValue)
        {
            EmailValue = emailValue;
        }

        public string EmailValue { get; } = default!;


        public static Result<Email> Create(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return $"{nameof(Email)} " + $"{nameof(email)}" + " can not be empty";
            }

            var regex = new Regex(EMAIL_REGULAR_EXPR);
            if (!regex.IsMatch(email))
            {
                return $"{nameof(Email)} has incorrect format";
            }

            var emailValue = new Email(email);

            return emailValue;
        }
    }
}
