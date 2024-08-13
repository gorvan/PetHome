namespace PetHome.Domain.Models
{
    public class Volunteer
    {
        public Guid Id { get; private set; }
        public Name Name { get; private set; }
        public string Description { get; private set; }
        public int Experience { get; private set; }
        public int FoundHomePets { get; private set; }
        public int NeedHomePets { get; private set; }
        public int TreatPets { get; private set; }
        public string Phone { get; private set; }
        public List<SocialNetwork> SocialNetworks { get; private set; }
        public List<Requisite> Detailes { get; private set; }
        public List<Pet> Pets { get; private set; }
    }
}
