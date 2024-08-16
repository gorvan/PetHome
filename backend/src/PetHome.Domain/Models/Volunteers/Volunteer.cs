using PetHome.Domain.Models.CommonModels;
using PetHome.Domain.Shared;
using PetHome.Domain.Models.Pets;

namespace PetHome.Domain.Models.Volunteers
{
    public class Volunteer : Entity<VolunteerId>
    {
        private Volunteer(VolunteerId id) : base(id)
        {
        }

        public FullName Name { get; private set; }
        public string Description { get; private set; }
        public int Experience { get; private set; }
        public int FoundHomePets { get; private set; }
        public int NeedHomePets { get; private set; }
        public int TreatPets { get; private set; }
        public string Phone { get; private set; }
        public SocialNetworkCollection SocialNetworks { get; private set; }
        public List<Requisite> Detailes { get; private set; }
        public List<Pet> Pets { get; private set; }
    }
}