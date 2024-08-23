using PetHome.Domain.PetManadgement.Entities;
using PetHome.Domain.PetManadgement.ValueObjects;
using PetHome.Domain.Shared;
using PetHome.Domain.Shared.IDs;

namespace PetHome.Domain.PetManadgement.AggregateRoot
{
    public class Volunteer : Entity<VolunteerId>
    {
        private Volunteer(VolunteerId id) : base(id)
        {
        }

        private Volunteer(
            VolunteerId id,
            FullName name,
            Email email,
            VolunteerDescription description,
            Phone phone,
            SocialNetworks socialNets,
            VolunteersRequisites requisites,
            IEnumerable<Pet> pets,
            int experience)
            : base(id)
        {
            Name = name;
            Email = email;
            Description = description;
            Phone = phone;
            SocialNets = socialNets;
            Requisites = requisites;
            _pets = pets.ToList();
            Experience = experience;
        }

        private readonly List<Pet> _pets = [];

        public FullName Name { get; private set; }
        public Email Email { get; private set; }
        public VolunteerDescription Description { get; private set; }
        public Phone Phone { get; private set; }
        public SocialNetworks SocialNets { get; private set; }
        public VolunteersRequisites Requisites { get; private set; }
        public IReadOnlyList<Pet> Pets => _pets;
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


        public static Result<Volunteer> Create(
            VolunteerId id,
            FullName name,
            Email email,
            VolunteerDescription description,
            Phone phone,
            SocialNetworks socialNets,
            VolunteersRequisites requisites,
            IEnumerable<Pet> pets,
            int experience)
        {
            var volunteer = new Volunteer(
                    id,
                    name,
                    email,
                    description,
                    phone,
                    socialNets,
                    requisites,
                    pets,
                    experience);

            return volunteer;
        }
    }
}