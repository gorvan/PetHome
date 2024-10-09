using PetHome.Domain.PetManadgement.ValueObjects;
using PetHome.Domain.Shared;
using PetHome.Domain.Shared.IDs;
using PetHome.Domain.SpeciesManagement.AggregateRoot;

namespace PetHome.Domain.PetManadgement.Entities
{
    public class Pet : Entity<PetId>, ISoftDeletable
    {
        private Pet(PetId id) : base(id)
        {
        }

        public Pet(
            PetId id,
            PetNickname nickname,
            SpeciesBreedValue speciesBreed,
            PetDescription description,
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
        public PetDescription Description { get; private set; } = default!;
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

        public Result<int> SetPhotos(IEnumerable<PetPhoto> petPhotos)
        {
            _photo = petPhotos.ToList();
            return _photo.Count;
        }

        public void DeletePhotos()
        {
            _photo = new List<PetPhoto>();
        }

        public void SetSerialNumber(SerialNumber serialNumber)
        {
            SerialNumber = serialNumber;
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
            if (_isDeleted)
            {
                _isDeleted = false;
            }
        }

        public void MoveUp()
        {
            SerialNumber = SerialNumber.Create(SerialNumber.Value + 1).Value;
        }

        public void MoveDown()
        {
            var newNumber = SerialNumber.Value - 1;
            if (newNumber < 1)
            {
                return;
            }

            SerialNumber = SerialNumber.Create(newNumber).Value;
        }

        public void Update(
            PetNickname petNickName,
            SpeciesBreedValue speciesBreedValue,
            PetDescription petDescription,
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

        public void UpdateHelpStatus(HelpStatus newHelpStatus)
        {
            HelpStatus = newHelpStatus;
        }
    }
}
