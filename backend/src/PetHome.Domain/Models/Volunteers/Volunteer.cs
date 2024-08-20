using PetHome.Domain.Models.CommonModels;
using PetHome.Domain.Models.Pets;
using PetHome.Domain.Shared;

namespace PetHome.Domain.Models.Volunteers
{
    public class Volunteer : Entity<VolunteerId>
    {        
        private readonly List<Pet> _pets = [];       

        private Volunteer(VolunteerId id) : base(id)
        {
        }

        private Volunteer(
            VolunteerId id,
            FullName name,
            Email email,
            Description descriptionValue,
            Phone phone,
            SocialNetworkCollection socialNetworkValue,
            RecuisiteCollection recuisiteCollectionValue) 
            : base(id)
        {
            Name = name;
            Email = email;
            DescriptionValue =descriptionValue;
            Phone = phone;
            SocialNetworksValue = socialNetworkValue;
            RequisiteCollectionValue = recuisiteCollectionValue;
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
        public RecuisiteCollection RequisiteCollectionValue { get; private set; }        
        public IReadOnlyList<Pet>? Pets =>_pets;

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
            RecuisiteCollection requisite)
        {
            var volunteerId = VolunteerId.NewVolunteerId();

            if (name is null)
            {
                return "Name can not be null";
            }

            if (email is null)
            {
                return "Email can not be null";
            }

            if (description is null)
            {
                return "Description can not be null";
            }

            if (phone is null)
            {
                return "Phone can not be null";
            }

            if (socialNetwork is null)
            {
                return "SocialNetwork can not be null";
            }

            if (requisite is null)
            {
                return "Requisite can not be null";
            }

            var volunteer = new Volunteer(volunteerId, name, email, 
                description, phone, socialNetwork, requisite);  
            return volunteer;
        }
    }
}