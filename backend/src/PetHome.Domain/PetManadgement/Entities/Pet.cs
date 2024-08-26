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

        public Pet(
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

        public PetNickname Nickname { get; } = default!;
        public SpeciesBreedValue SpeciesBreed { get; } = default!;
        public PetDescription Description { get; } = default!;
        public PetColor Color { get; } = default!;
        public HealthInfo Health { get; } = default!;
        public Address Address { get; } = default!;
        public Phone Phone { get; } = default!;
        public DateValue BirthDay { get; } = default!;
        public DateValue CreateDate { get; } = default!;
        public PetRequisites Requisites { get; } = default!;
        public bool IsNeutered { get; }
        public bool IsVaccinated { get; }
        public HelpStatus HelpStatus { get; }
        public double Weight { get; } = 0;
        public double Height { get; } = 0;
        public IReadOnlyList<PetPhoto> Photos => _photo;
    }
}
