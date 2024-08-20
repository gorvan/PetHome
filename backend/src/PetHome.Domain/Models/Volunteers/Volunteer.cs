using PetHome.Domain.Models.CommonModels;
using PetHome.Domain.Models.Pets;
using PetHome.Domain.Shared;

namespace PetHome.Domain.Models.Volunteers
{
    public class Volunteer : Entity<VolunteerId>
    {
        private readonly List<Requisite> _detailes = [];
        private readonly List<Pet> _pets = [];       

        private Volunteer(VolunteerId id) : base(id)
        {
        }

        public FullName Name { get; private set; }
        public Email Email { get; private set; }
        public Description DescriptionValue { get; private set; }
        public int Experience { get; private set; }
        public int FoundHomePets =>
            _pets
            .Where(p => p.HelpStatus == HelpStatus.FoundHome)
            .Count();
        public int NeedHomePets =>
            _pets
            .Where(p => p.HelpStatus == HelpStatus.NeeedHome)
            .Count();
        public int TreatPets =>
            _pets
            .Where(p => p.HelpStatus == HelpStatus.OnTreatment)
            .Count();
        public Phone Phone { get; private set; }
        public SocialNetworkCollection SocialNetworksValue { get; private set; }
        public IReadOnlyList<Requisite> Detailes =>_detailes;
        public IReadOnlyList<Pet> Pets =>_pets;

        public void AddRequisite(Requisite requisite)
        {
            _detailes.Add(requisite);
        }

        public void AddPet(Pet pet)
        {
            _pets.Add(pet);
        }

        public static Result<Volunteer> Create(
            FullName name, 
            Email email, 
            Description description, 
            Phone phone, 
            SocialNetworkCollection socialNetwork,
            Requisite requisite)
        {
            var volunteerId = VolunteerId.NewVolunteerId();
            var volunteer = new Volunteer(volunteerId);
            
            volunteer.Name = name;
            volunteer.Email = email;
            volunteer.DescriptionValue = description;
            volunteer.Phone = phone;
            volunteer.SocialNetworksValue = socialNetwork;  
            volunteer.AddRequisite(requisite);

            return volunteer;
        }
    }
}