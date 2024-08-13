namespace PetHome.Domain.Models
{
    public record Adress
    {        
        public string City { get; private set; }
        public string Street { get; private set; }
        public string HouseNumber { get; private set; }
        public string AppartmentNumber { get; private set; }
    }
}
