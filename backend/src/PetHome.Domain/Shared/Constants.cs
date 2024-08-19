namespace PetHome.Domain.Shared
{
    public class Constants
    {
        public const int MAX_TITLE_LENGTH = 100;
        public const int MAX_TEXT_LENGTH = 2000;
        public const int MAX_WORD_LENGTH = 100;

        public const string PHONE_REGULAR_EXPR = @"^(\+)?((\d{2,3}) ?\d|\d)(([ -]?\d)|( ?(\d{2,3}) ?)){5,12}\d$";
        public const string EMAIL_REGULAR_EXPR = @"^[-\w.]+@([A-z0-9][-A-z0-9]+\.)+[A-z]{2,4}$";
    }
}
