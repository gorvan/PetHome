using PetHome.Domain.Shared;

namespace PetHome.Domain.PetManadgement.ValueObjects
{
    public record Address
    {
        private Address(
            string city,
            string street,
            string houseNumber,
            string appartmentNumber)
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

        public static Result<Address> Create(
            string city,
            string street,
            string house,
            string appartment)
        {
            if (string.IsNullOrWhiteSpace(city))
            {
                return Errors.General.ValueIsInvalid("Address.City");
            }

            if (string.IsNullOrWhiteSpace(street))
            {
                return Errors.General.ValueIsInvalid("Address.Street");
            }

            if (string.IsNullOrWhiteSpace(house))
            {
                return Errors.General.ValueIsInvalid("Address.House");
            }

            if (string.IsNullOrWhiteSpace(appartment))
            {
                return Errors.General.ValueIsInvalid("Address.Appartment");
            }

            return new Address(city, street, house, appartment);
        }
    }
}
