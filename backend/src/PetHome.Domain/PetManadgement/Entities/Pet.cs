using PetHome.Domain.PetManadgement.ValueObjects;
using PetHome.Domain.Shared;
using PetHome.Domain.Shared.IDs;
using PetHome.Domain.SpeciesManagement.AggregateRoot;

namespace PetHome.Domain.PetManadgement.Entities
{
    public class Pet : Entity<PetId>
    {
        private Pet(PetId id) : base(id)
        {
        }

        private Pet(
            PetId id,
            PetNickname nickname,
            SpeciesBreedValue speciesBreed,
            PetDescription description,
            PetColor color,
            HealthInfo health,
            Address address,
            Phone phone,
            PetRequisites requisites,
            DateValue birthDay,
            DateValue createDate,
            bool isNeutered,
            bool isVaccinated,
            HelpStatus helpStatus,
            double weight,
            double height,
            IEnumerable<PetPhoto> photos)
            : base(id)
        {
            Nickname = nickname;
            SpeciesBreed = speciesBreed;
            Description = description;
            Color = color;
            Health = health;
            Address = address;
            Phone = phone;
            Requisites = requisites;
            BirthDay = birthDay;
            CreateDate = createDate;
            IsNeutered = isNeutered;
            IsVaccinated = isVaccinated;
            HelpStatus = helpStatus;
            Weight = weight;
            Height = height;
            _photo = photos.ToList();
        }

        private readonly List<PetPhoto> _photo = [];

        public PetNickname Nickname { get; private set; }
        public SpeciesBreedValue SpeciesBreed { get; private set; }
        public PetDescription Description { get; private set; }
        public PetColor Color { get; private set; }
        public HealthInfo Health { get; private set; }
        public Address Address { get; private set; }
        public Phone Phone { get; private set; }
        public DateValue BirthDay { get; private set; }
        public DateValue CreateDate { get; private set; }
        public PetRequisites Requisites { get; private set; }
        public bool IsNeutered { get; private set; }
        public bool IsVaccinated { get; private set; }
        public HelpStatus HelpStatus { get; private set; }
        public double Weight { get; private set; }
        public double Height { get; private set; }
        public IReadOnlyList<PetPhoto> Photos => _photo;


        public void AddPhoto(PetPhoto photo)
        {
            _photo.Add(photo);
        }

        public static Result<Pet> Create(
            PetId id,
            PetNickname nickname,
            SpeciesBreedValue speciesBreed,
            PetDescription description,
            PetColor color,
            HealthInfo health,
            Address address,
            Phone phone,
            PetRequisites requisites,
            DateValue birthDay,
            DateValue createDate,
            bool isNeutered,
            bool isVaccinated,
            HelpStatus helpStatus,
            double weight,
            double height,
            IEnumerable<PetPhoto> photos)
        {

            if (nickname is null)
            {
                return "Nickname can not be null";
            }

            if (speciesBreed is null)
            {
                return "SpeciesBreedValue can not be null";
            }

            if (description is null)
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

            if (requisites is null)
            {
                return "Requisites can not be null";
            }

            if (birthDay is null)
            {
                return "BirthDay can not be null";
            }

            if (createDate is null)
            {
                return "CreateDate can not be null";
            }

            if (photos is null)
            {
                return "Photos can not be null";
            }

            var petValue = new Pet(
                id,
                nickname,
                speciesBreed,
                description,
                color,
                health,
                address,
                phone,
                requisites,
                birthDay,
                createDate,
                isNeutered,
                isVaccinated,
                helpStatus,
                weight,
                height,
                photos);

            return petValue;
        }
    }
}
