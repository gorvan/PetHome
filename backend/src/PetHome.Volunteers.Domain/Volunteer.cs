using PetHome.Shared.Core.Shared;
using PetHome.Shared.Core.Shared.IDs;
using PetHome.Shared.Framework.Entities;
using PetHome.Volunteers.Domain.Entities;
using PetHome.Volunteers.Domain.ValueObjects;

namespace PetHome.Volunteers.Domain
{
    public class Volunteer : SoftDeletableEntity<VolunteerId>
    {
        private Volunteer(VolunteerId id) : base(id)
        {
        }

        public Volunteer(
            VolunteerId id,
            FullName name,
            Email email,
            DescriptionValueObject description,
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
            Experience = experience;
        }

        private readonly List<Pet> _pets = [];        

        public FullName Name { get; private set; } = default!;
        public Email Email { get; private set; } = default!;
        public DescriptionValueObject Description { get; private set; } = default!;
        public Phone Phone { get; private set; } = default!;        
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
            DescriptionValueObject description)
        {
            Name = fullName;
            Email = email;
            Phone = phone;
            Description = description;
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
        public override void Delete()
        {
            base.Delete();

            foreach (var pet in _pets)
            {
                pet.Delete();
            }
        }

        public override void Restore()
        {
            base.Restore();

            foreach (var pet in _pets)
            {
                pet.Restore();
            }
        }

        public void DeleteExpiredPets() => _pets.RemoveAll(p => p.IsExpired);

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

        public Result<int> AddPetFiles(Pet pet, IReadOnlyList<PetPhoto> petPhotos)
        {
            var result = pet.SetPhotos(petPhotos);
            if (result.IsFailure)
            {
                return result.Error;
            }

            return result;
        }

        public Result<int> UpdatePetFiles(Pet pet, IReadOnlyList<PetPhoto> petPhotos)
        {
            pet.DeletePhotos();

            var result = pet.SetPhotos(petPhotos);
            if (result.IsFailure)
            {
                return result.Error;
            }

            return result;
        }

        public Result UpdatePetStatus(Guid petId, HelpStatus helpStatus)
        {
            var pet = _pets.FirstOrDefault(p => p.Id.Id == petId);

            if (pet == null)
            {
                return Errors.General.NotFound(petId);
            }

            pet.UpdateHelpStatus(helpStatus);

            return Result.Success();
        }

        public Result UpdatePet(
            Guid petId,
            string nickName,
            SpeciesBreedValue speciesBreedValue,
            string description,
            string color,
            string health,
            string city,
            string street,
            string houseNumber,
            string appartmentNumber,
            string phone,
            string requisiteName,
            string requisiteDescription,
            DateTime birthday,
            bool isNeutered,
            bool isVaccinated,
            HelpStatus helpStatus,
            double weight,
            double height)
        {
            var pet = _pets.FirstOrDefault(p => p.Id.Id == petId);
            if (pet == null)
            {
                return Errors.General.NotFound(petId);
            }

            var petNickName = PetNickname.Create(nickName).Value;

            var petDescription = DescriptionValueObject.Create(description).Value;

            var petColor = PetColor.Create(color).Value;

            var healthInfo = HealthInfo.Create(health).Value;

            var address = Address.Create(
                city,
                street,
                houseNumber,
                appartmentNumber).Value;

            var phoneValue = Phone.Create(phone).Value;

            var requisite = Requisite.Create(
                requisiteName,
                requisiteDescription);

            var birthdayValue = DateValue.Create(birthday).Value;

            var createDate = DateValue.Create(DateTime.UtcNow).Value;

            pet.Update(
            petNickName,
            speciesBreedValue,
            petDescription,
            petColor,
            healthInfo,
            address,
            phoneValue,
            [requisite.Value],
            birthdayValue,
            isNeutered,
            isVaccinated,
            helpStatus,
            weight,
            height);

            return Result.Success();
        }

        public Result SetMainPetPhoto(Guid petId, string filePath)
        {
            var petResult = _pets.FirstOrDefault(p => p.Id.Id == petId);
            if (petResult == null)
            {
                return Errors.General.NotFound(petId);
            }

            return petResult.SetMainPhoto(filePath);
        }
    }
}