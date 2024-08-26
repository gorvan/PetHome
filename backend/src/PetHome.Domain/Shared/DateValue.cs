namespace PetHome.Domain.Shared
{
    public record DateValue
    {
        private DateValue(DateTime date)
        {
            Date = date;
        }

        public DateTime Date { get; } = default!;

        public static Result<DateValue> Create(DateTime date)
        {
            if (date < new DateTime(1900, 1, 1)
                || date > DateTime.Now)
            {
                return Errors.General.ValueIsInvalid("Date");
            }

            return new DateValue(date);
        }
    }
}
