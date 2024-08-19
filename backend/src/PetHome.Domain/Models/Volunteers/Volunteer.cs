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
        public int FoundHomePets { get; private set; }
        public int NeedHomePets { get; private set; }
        public int TreatPets { get; private set; }
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

        public static Result<Volunteer> Create(FullName name, Email email, 
            Description description, Phone phone, SocialNetworkCollection socialNetwork,
            Requisite requisite)
        {
            var volunteerId = VolunteerId.NewVolunteerId();
            var volunteer = new Volunteer(volunteerId);

            if(name is null)
            {
                return "Name can not be null";
            }
            volunteer.Name = name;

            if (email is null)
            {
                return "Email can not be null";
            }
            volunteer.Email = email;

            if (description is null)
            {
                return "Description can not be null";
            }
            volunteer.DescriptionValue = description;

            if (phone is null)
            {
                return "Phone can not be null";
            }
            volunteer.Phone = phone;

            if (socialNetwork is null)
            {
                return "SocialNetwork can not be null";
            }
            volunteer.SocialNetworksValue = socialNetwork;            

            if (requisite is null)
            {
                return "Requisite can not be null";
            }
            volunteer.AddRequisite(requisite);

            return volunteer;
        }
    }
}