namespace PetHome.Domain.Models
{
    public record Adress
    {        
        public string City { get; }
        public string Street { get; }
        public string HouseNumber { get; }
        public string AppartmentNumber { get; }
    }
}
