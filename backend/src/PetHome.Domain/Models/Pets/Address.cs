using PetHome.Domain.Shared;

namespace PetHome.Domain.Models.Pets
{
    public record Address
    {
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

        public static Result<Address> Create(string city, string street, string house, string appartment)
        {
            if (string.IsNullOrWhiteSpace(city))
            {
                return $"{nameof(Address)} " + $"{nameof(city)}" + " can not be empty";
            }

            if (string.IsNullOrWhiteSpace(street))
            {
                return $"{nameof(Address)} " + $"{nameof(street)}" + " can not be empty";
            }

            if (string.IsNullOrWhiteSpace(house))
            {
                return $"{nameof(Address)} " + $"{nameof(house)}" + " can not be empty";
            }

            if (string.IsNullOrWhiteSpace(appartment))
            {
                return $"{nameof(Address)} " + $"{nameof(appartment)}" + " can not be empty";
            }

            var adress = new Address(city, street, house, appartment);

            return adress;
        }
    }
}
