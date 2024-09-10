using Microsoft.Extensions.Logging;
using PetHome.Domain.PetManadgement.AggregateRoot;
using PetHome.Domain.PetManadgement.Entities;
using PetHome.Domain.PetManadgement.ValueObjects;
using PetHome.Domain.Shared;
using PetHome.Domain.Shared.IDs;
using PetHome.Domain.SpeciesManagement.AggregateRoot;

namespace PetHome.Application.Volunteers.AddPet
{
    public class AddPetHandler
    {
        private readonly IVolunteerRepository _volunteerRepository;
        private readonly ILogger<AddPetHandler> _logger;

        public AddPetHandler(
            IVolunteerRepository volunteerRepository,
            ILogger<AddPetHandler> logger)
        {
            _volunteerRepository = volunteerRepository;
            _logger = logger;
        }

        public async Task<Result<Guid>> Execute(
            AddPetCommand command,
            CancellationToken token)
        {
            var volunteerResult = await _volunteerRepository
                .GetById(VolunteerId.Create(command.VolunteerId), token);

            if (volunteerResult.IsFailure)
            {
                return volunteerResult.Error;
            }

            var pet = CreatePet(command);

            volunteerResult.Value.AddPet(pet);

            await _volunteerRepository.Update(volunteerResult.Value, token);

            _logger.LogInformation("Add new pet, Id: {petId}", pet.Id);

            return pet.Id.Id;
        }

        private Pet CreatePet(AddPetCommand command)
        {
            var petId = PetId.NewPetId();

            var petNickName =
                PetNickname.Create(command.Nickname).Value;

            var speciesId = SpeciesId.Empty();

            var breedId = BreedId.Empty();

            var speciesBreedValue =
                new SpeciesBreedValue(speciesId, breedId);

            var petDescription =
                PetDescription.Create(command.Description).Value;

            var petColor = PetColor.Create(command.Color).Value;

            var healthInfo =
                HealthInfo.Create(command.Health).Value;

            var address = Address.Create(
                command.Address.City,
                command.Address.Street,
                command.Address.HouseNumber,
                command.Address.AppartmentNumber).Value;

            var phone = Phone.Create(command.Phone).Value;

            var requisite = Requisite.Create(
                command.Requisites.Name,
                command.Requisites.Description);

            var petRequisites =
                new PetRequisites([requisite.Value]);

            var birthday = DateValue.Create(command.BirthDay).Value;

            var createDate = DateValue.Create(DateTime.UtcNow).Value;

            return new Pet(
                petId,
                petNickName,
                speciesBreedValue,
                petDescription,
                petColor,
                healthInfo,
                address,
                phone,
                petRequisites,
                birthday,
                createDate,
                command.IsNeutered,
                command.IsVaccinated,
                command.HelpStatus,
                command.Weight,
                command.Height);
        }
    }
}
