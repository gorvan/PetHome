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

        public PetNickname Nickname { get; } = default!;
        public SpeciesBreedValue SpeciesBreed { get; } = default!;
        public PetDescription Description { get; } = default!;
        public PetColor Color { get; } = default!;
        public HealthInfo Health { get; } = default!;
        public Address Address { get; } = default!;
        public Phone Phone { get; } = default!;
        public DateValue BirthDay { get; } = default!;
        public DateValue CreateDate { get; } = default!;
        public IReadOnlyList<Requisite> Requisites { get; } = default!;
        public bool IsNeutered { get; }
        public bool IsVaccinated { get; }
        public HelpStatus HelpStatus { get; }
        public double Weight { get; } = 0;
        public double Height { get; } = 0;
        public SerialNumber SerialNumber { get; private set; } = default!;

        public IReadOnlyList<PetPhoto> Photos => _photo;
           
        public Result<int> SetPhotos(IEnumerable<PetPhoto> petPhotos)
        {
            _photo = petPhotos.ToList();
            return _photo.Count;
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
    }
}
