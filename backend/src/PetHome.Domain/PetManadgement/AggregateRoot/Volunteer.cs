using PetHome.Domain.PetManadgement.Entities;
using PetHome.Domain.PetManadgement.ValueObjects;
using PetHome.Domain.Shared;
using PetHome.Domain.Shared.IDs;

namespace PetHome.Domain.PetManadgement.AggregateRoot
{
    public class Volunteer : Entity<VolunteerId>, ISoftDeletable
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
            SocialNetworks = socialNets;
            Requisites = requisites;
        }

        private readonly List<Pet> _pets = [];

        private bool _isDeleted = false;


        public FullName Name { get; private set; } = default!;
        public Email Email { get; private set; } = default!;
        public VolunteerDescription Description { get; private set; } = default!;
        public Phone Phone { get; private set; } = default!;
        public SocialNetworks SocialNetworks { get; private set; } = default!;
        public VolunteersRequisites Requisites { get; private set; } = default!;
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
        
        public void UpdateMainInfo(
            FullName fullName, 
            Email email, 
            Phone phone, 
            VolunteerDescription description )
        {
            Name = fullName;
            Email = email;
            Phone = phone;
            Description = description;
        }

        public void UpdateRequisites(
            VolunteersRequisites requisites)
        {
            Requisites = requisites;
        }

        public void UpdateSocialNetworks(
            SocialNetworks socialNetworks)
        {
            SocialNetworks = socialNetworks;
        }

        public void Delete()
        {
            if (_isDeleted == false)
            {
                _isDeleted = true;
            }
        }

        public void Restore()
        {
            if(_isDeleted)
            {
                _isDeleted = false;
            }            
        }
    }    
}