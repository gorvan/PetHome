using PetHome.Domain.Models.CommonModels;
using PetHome.Domain.Shared;

namespace PetHome.Domain.Models.Pets
{
    public class Pet : Entity<PetId>
    {
        private readonly List<Requisite> _detailes = [];
        private readonly List<PetPhoto> _photo = [];

        private Pet(PetId id) : base(id)
        {
        }

        private Pet(
            PetId id,
            NotNullableString nickname,
            SpeciesBreedValue speciesBreedValue,
            Description descriptionValue,
            NotNullableString color,
            Description health,
            Address address,
            Phone phone,
            bool isneutered,
            DateTimeValue birthDay,
            bool isVaccinated,
            HelpStatus helpStatus)
            : base(id)
        {
            Nickname = nickname;
            SpeciesBreedValue = speciesBreedValue;
            DescriptionValue = descriptionValue;
            Color = color;
            Health = health;
            Address = address;
            Phone = phone;
            IsNeutered = isneutered;
            BirthDay = birthDay;
            IsVaccinated = isVaccinated;
            HelpStatus = helpStatus;
        }

        public NotNullableString Nickname { get; private set; }
        public SpeciesBreedValue SpeciesBreedValue { get; private set; }
        public Description DescriptionValue { get; private set; }
        public NotNullableString Color { get; private set; }
        public Description Health { get; private set; }
        public Address Address { get; private set; }
        public double Weight { get; private set; }
        public double Height { get; private set; }
        public Phone Phone { get; private set; }
        public bool IsNeutered { get; private set; }
        public DateTimeValue BirthDay { get; private set; }
        public bool IsVaccinated { get; private set; }
        public HelpStatus HelpStatus { get; private set; }
        public IReadOnlyList<Requisite> Detailes => _detailes;
        public DateTime CreateTime { get; private set; }
        public IReadOnlyList<PetPhoto> Photos => _photo;


        public void AddRequisite(Requisite requisite)
        {
            _detailes.Add(requisite);
        }

        public void AddPhoto(PetPhoto photo)
        {
            _photo.Add(photo);
        }
        
        public static Result<Pet> Create(
            PetId petId,
            NotNullableString nickname, 
            SpeciesBreedValue speciesBreedValue,
            Description descriptionValue, 
            NotNullableString color, 
            Description health,
            Address address, 
            Phone phone, 
            bool isneutered, 
            DateTimeValue birthDay, 
            bool isVaccinated,
            HelpStatus helpStatus)
        {           

            if (nickname is null)
            {
                return "Nickname can not be null";
            }

            if (speciesBreedValue is null)
            {
                return "SpeciesBreedValue can not be null";
            }

            if (descriptionValue is null)
            {
                return "DescriptionValue can not be null";
            }

            if (color is null)
            {
                return "Color can not be null";
            }

            if (health is null)
            {
                return "Health can not be null";
            }

            if (address is null)
            {
                return "Address can not be null";
            }

            if (phone is null)
            {
                return "Phone can not be null";
            }

            if (birthDay is null)
            {
                return "BirthDay can not be null";
            }

            var pet = new Pet(petId, nickname, speciesBreedValue, 
                descriptionValue, color, health, address, phone, 
                isneutered, birthDay, isVaccinated, helpStatus);
            
            pet.CreateTime = DateTime.Now;
            return pet;
        }
    }
}
