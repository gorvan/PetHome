using PetHome.Shared.Core.Shared;
using System.Text.RegularExpressions;

namespace PetHome.Volunteers.Domain.ValueObjects
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
                return Errors.General.ValueIsRequeired("Email");
            }

            var regex = new Regex(EMAIL_REGULAR_EXPR);
            if (!regex.IsMatch(email))
            {
                return Errors.General.ValueIsInvalid("Email");
            }

            return new Email(email);
        }
    }
}
