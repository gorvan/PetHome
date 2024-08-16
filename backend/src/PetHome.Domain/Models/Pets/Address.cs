namespace PetHome.Domain.Models.Pets
{
    public record Address
    {
        public const int MAX_LENGTH = 100;
        private Address() { }

        private Address(string city, string street, string houseNumber, string appartmentNumber)
        {
            City = city;
            Street = street;
            HouseNumber = houseNumber;
            AppartmentNumber = appartmentNumber;
        }

        public string City { get; } = default!;
        public string Street { get; } = default!;
        public string HouseNumber { get; } = default!;
        public string AppartmentNumber { get; } = default!;

        public static Address Create(string city, string street, string house, string appartment)
            => new(city, street, house, appartment);
    }
}
