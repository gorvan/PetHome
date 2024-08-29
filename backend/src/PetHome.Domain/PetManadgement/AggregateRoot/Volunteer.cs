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

        public Volunteer(
            VolunteerId id,
            FullName name,
            Email email,
            VolunteerDescription description,
            Phone phone,
            SocialNetworks socialNets,
            VolunteersRequisites requisites)
            : base(id)
        {
            Name = name;
            Email = email;
            Description = description;
            Phone = phone;
            SocialNets = socialNets;
            Requisites = requisites;
        }

        private readonly List<Pet> _pets = [];

        public FullName Name { get; } = default!;
        public Email Email { get; } = default!;
        public VolunteerDescription Description { get; } = default!;
        public Phone Phone { get; } = default!;
        public SocialNetworks SocialNets { get; } = default!;
        public VolunteersRequisites Requisites { get; } = default!;
        public IReadOnlyList<Pet> Pets => _pets;
        
        public int Experience => GetExperience();

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

        private int GetExperience()
        {
            var firstPet = 
                _pets
                .OrderBy(p => p.CreateDate.Date)
                .FirstOrDefault();
            
            if (firstPet == null)
            {
                return 0;
            }

            return DateTime.Now.Year - firstPet.CreateDate.Date.Year;
        }        
    }

    
}