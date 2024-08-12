namespace PetHome.Domain.Models
{
    public class AdressInfo
    {
        public Guid PetId { get; }
        public string City { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public string AppartmentNumber { get; set; }

        public string GetMapString() =>
             $"{City},{Street},{HouseNumber}";

    }
}
