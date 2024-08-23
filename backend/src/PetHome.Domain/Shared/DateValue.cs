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
            if (date == DateTime.MinValue || date == DateTime.MaxValue)
            {
                return "Date is invalid";
            }

            if (date > DateTime.Now)
            {
                return "Date can not be more than now";
            }

            var dateValue = new DateValue(date);

            return dateValue;
        }
    }
}
