using PetHome.Shared.Core.Shared;
using PetHome.Shared.Core.Shared.IDs;
using PetHome.Volunteers.Domain.Entities;
using PetHome.Volunteers.Domain.ValueObjects;

namespace PetHome.Volunteers.Domain
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
            Description description,
            Phone phone,
            IEnumerable<SocialNetwork> socialNets,
            IEnumerable<Requisite> requisites,
            int experience)
            : base(id)
        {
            Name = name;
            Email = email;
            Description = description;
            Phone = phone;
            SocialNetworks = socialNets.ToList();
            Requisites = requisites.ToList();
            Experience = experience;
        }

        private readonly List<Pet> _pets = [];
        private bool _isDeleted = false;

        public FullName Name { get; private set; } = default!;
        public Email Email { get; private set; } = default!;
        public Description Description { get; private set; } = default!;
        public Phone Phone { get; private set; } = default!;
        public IReadOnlyList<SocialNetwork> SocialNetworks { get; private set; } = default!;
        public IReadOnlyList<Requisite> Requisites { get; private set; } = default!;
        public int Experience { get; private set; } = default!;
        public IReadOnlyList<Pet> Pets => _pets;

        public int FoundHomePets => _pets
            .Count(p => p.HelpStatus == HelpStatus.FoundHome);

        public int NeedHomePets => _pets
            .Count(p => p.HelpStatus == HelpStatus.NeeedHome);

        public int TreatPets => _pets
            .Count(p => p.HelpStatus == HelpStatus.OnTreatment);

        public void UpdateMainInfo(
            FullName fullName,
            Email email,
            Phone phone,
            Description description)
        {
            Name = fullName;
            Email = email;
            Phone = phone;
            Description = description;
        }

        public void UpdateRequisites(IEnumerable<Requisite> requisites)
        {
            Requisites = requisites.ToList();
        }

        public void UpdateSocialNetworks(IEnumerable<SocialNetwork> socialNetworks)
        {
            SocialNetworks = socialNetworks.ToList();
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

        public Result<Guid> AddPet(Pet pet)
        {
            _pets.Add(pet);

            var serialNumberResult = SerialNumber.Create(_pets.Count);
            if (serialNumberResult.IsFailure)
            {
                return serialNumberResult.Error;
            }

            pet.SetSerialNumber(serialNumberResult.Value);

            return pet.Id.Id;
        }

        public void MovePet(Pet pet, SerialNumber serialNumber)
        {
            if (pet.SerialNumber == serialNumber || _pets.Count < 2)
            {
                return;
            }

            var sortedPets = _pets.OrderBy(p => p.SerialNumber.Value).ToList();

            if (pet.SerialNumber.Value > serialNumber.Value)
            {
                for (int i = pet.SerialNumber.Value - 2; i >= serialNumber.Value - 1; i--)
                {
                    sortedPets[i].MoveUp();
                }
            }
            else
            {
                for (int i = pet.SerialNumber.Value - 1; i < serialNumber.Value; i++)
                {
                    sortedPets[i].MoveDown();
                }
            }

            pet.SetSerialNumber(serialNumber);
        }

        public Result SoftDeletePet(Pet pet)
        {
            _pets.Remove(pet);
            pet.Delete();
            return UpdatePetsPositions();
        }

        public Result FullDeletePet(Pet pet)
        {
            _pets.Remove(pet);
            return UpdatePetsPositions();
        }

        private Result UpdatePetsPositions()
        {
            if (_pets.Count < 2)
            {
                return Result.Success();
            }

            var sortedPets = _pets.OrderBy(p => p.SerialNumber.Value).ToList();

            for (int i = 0; i < sortedPets.Count; i++)
            {
                var pet = sortedPets[i];

                if (pet.SerialNumber.Value == i + 1)
                {
                    continue;
                }

                var serialNumberResult = SerialNumber.Create(i);

                if (serialNumberResult.IsFailure)
                {
                    return serialNumberResult.Error;
                }

                pet.SetSerialNumber(serialNumberResult.Value);
            }

            return Result.Success();
        }
    }
}