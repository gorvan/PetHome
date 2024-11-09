using PetHome.Shared.Core.Shared;
using PetHome.Shared.Core.Shared.IDs;
using PetHome.Shared.Framework.Entities;
using PetHome.Volunteers.Domain.ValueObjects;

namespace PetHome.Volunteers.Domain.Entities
{
    public class Pet : SoftDeletableEntity<PetId>
    {
        private Pet(PetId id) : base(id)
        {
        }

        public Pet(
            PetId id,
            PetNickname nickname,
            SpeciesBreedValue speciesBreed,
            DescriptionValueObject description,
            PetColor color,
            HealthInfo health,
            Address address,
            Phone phone,
            IEnumerable<Requisite> requisites,
            DateValue birthDay,
            DateValue createDate,
            bool isNeutered,
            bool isVaccinated,
            HelpStatus helpStatus,
            double weight,
            double height)
            : base(id)
        {
            Nickname = nickname;
            SpeciesBreed = speciesBreed;
            Description = description;
            Color = color;
            Health = health;
            Address = address;
            Phone = phone;
            Requisites = requisites.ToList();
            BirthDay = birthDay;
            CreateDate = createDate;
            IsNeutered = isNeutered;
            IsVaccinated = isVaccinated;
            HelpStatus = helpStatus;
            Weight = weight;
            Height = height;
        }

        private List<PetPhoto> _photo = [];
        private bool _isDeleted = false;

        public PetNickname Nickname { get; private set; } = default!;
        public SpeciesBreedValue SpeciesBreed { get; private set; } = default!;
        public DescriptionValueObject Description { get; private set; } = default!;
        public PetColor Color { get; private set; } = default!;
        public HealthInfo Health { get; private set; } = default!;
        public Address Address { get; private set; } = default!;
        public Phone Phone { get; private set; } = default!;
        public DateValue BirthDay { get; private set; } = default!;
        public DateValue CreateDate { get; } = default!;
        public IReadOnlyList<Requisite> Requisites { get; private set; } = default!;
        public bool IsNeutered { get; private set; }
        public bool IsVaccinated { get; private set; }
        public HelpStatus HelpStatus { get; private set; }
        public double Weight { get; private set; } = 0;
        public double Height { get; private set; } = 0;
        public SerialNumber SerialNumber { get; private set; } = default!;

        public IReadOnlyList<PetPhoto> Photos => _photo;

        internal Result<int> SetPhotos(IEnumerable<PetPhoto> petPhotos)
        {
            _photo = petPhotos.ToList();
            return _photo.Count;
        }

        internal void DeletePhotos()
        {
            _photo = [];
        }

        internal void SetSerialNumber(SerialNumber serialNumber)
        {
            SerialNumber = serialNumber;
        }

        internal void MoveUp()
        {
            SerialNumber = SerialNumber.Create(SerialNumber.Value + 1).Value;
        }

        internal void MoveDown()
        {
            var newNumber = SerialNumber.Value - 1;
            if (newNumber < 1)
            {
                return;
            }

            SerialNumber = SerialNumber.Create(newNumber).Value;
        }

        internal void Update(
            PetNickname petNickName,
            SpeciesBreedValue speciesBreedValue,
            DescriptionValueObject petDescription,
            PetColor petColor,
            HealthInfo healthInfo,
            Address address,
            Phone phone,
            IEnumerable<Requisite> requisites,
            DateValue birthday,
            bool isNeutered,
            bool isVaccinated,
            HelpStatus helpStatus,
            double weight,
            double height)
        {
            Nickname = petNickName;
            SpeciesBreed = speciesBreedValue;
            Description = petDescription;
            Color = petColor;
            Health = healthInfo;
            Address = address;
            Phone = phone;
            BirthDay = birthday;
            Requisites = requisites.ToList();
            IsNeutered = isNeutered;
            IsVaccinated = isVaccinated;
            HelpStatus = helpStatus;
            Weight = weight;
            Height = height;
        }

        internal void UpdateHelpStatus(HelpStatus newHelpStatus)
        {
            HelpStatus = newHelpStatus;
        }

        internal Result<bool> SetMainPhoto(string filePath)
        {
            var photoResult = _photo.FirstOrDefault(p => p.Path.Path == filePath);
            if (photoResult == null)
            {
                return Errors.Files.NotFound(filePath);
            }

            _photo.ForEach(p => p.ResetMain());

            photoResult.SetMain();

            return true;
        }
    }
}
