using PetHome.Domain.Shared;

namespace PetHome.Domain.Models.CommonModels
{
    public record DateTimeValue
    {
        private DateTimeValue(DateTime date)
        {
            Date = date;
        }

        public DateTime Date { get; } = default!;

        public static Result<DateTimeValue> Create(DateTime date)
        {
            if(date == DateTime.MinValue || date == DateTime.MaxValue)
            {
                return "Date is invalid";
            }

            if(date > DateTime.Now)
            {
                return "Date can not be more than now";
            }

            var dateValue = new DateTimeValue(date);

            return dateValue;
        }
    }
}
