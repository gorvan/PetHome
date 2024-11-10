using FluentValidation;
using Microsoft.Extensions.Logging;
using PetHome.Shared.Core.Abstractions;
using PetHome.Shared.Core.Extensions;
using PetHome.Shared.Core.Shared;
using PetHome.Shared.Core.Shared.IDs;
using PetHome.Species.Contracts;
using PetHome.Volunteers.Domain.Entities;
using PetHome.Volunteers.Domain.ValueObjects;


namespace PetHome.Volunteers.Application.VolunteersManagement.Commands.PetManagement.AddPet
{
    public class AddPetHandler : ICommandHandler<Guid, AddPetCommand>
    {
        private readonly IVolunteerRepository _volunteerRepository;
        private readonly ISpeciesContract _speciesContract;
        private readonly ILogger<AddPetHandler> _logger;
        private readonly IValidator<AddPetCommand> _validator;

        public AddPetHandler(
            IVolunteerRepository volunteerRepository,
            ISpeciesContract speciesContract,
            ILogger<AddPetHandler> logger,
            IValidator<AddPetCommand> validator)
        {
            _volunteerRepository = volunteerRepository;
            _speciesContract = speciesContract;
            _logger = logger;
            _validator = validator;
        }

        public async Task<Result<Guid>> Execute(
            AddPetCommand command,
            CancellationToken token)
        {
            var validationResult = await _validator.ValidateAsync(command, token);
            if (validationResult.IsValid == false)
            {
                return validationResult.ToErrorList();
            }

            var volunteerResult = await _volunteerRepository
                .GetById(VolunteerId.Create(command.VolunteerId), token);

            if (volunteerResult.IsFailure)
            {
                return volunteerResult.Error;
            }

            var petResult = CreatePet(command);

            if (petResult.IsFailure)
            {
                return petResult.Error;
            }

            volunteerResult.Value.AddPet(petResult.Value);

            await _volunteerRepository.Update(volunteerResult.Value, token);

            _logger.LogInformation("Add new pet, Id: {petId}", petResult.Value.Id);

            return petResult.Value.Id.Id;
        }

        private Result<Pet> CreatePet(AddPetCommand command)
        {
            var petId = PetId.NewPetId();

            var petNickName =
                PetNickname.Create(command.Nickname).Value;

            var speciesBreedResult = GetSpeciesAndBreed(command);

            var speciesBreedValue =
                speciesBreedResult.IsSuccess
                ? _speciesContract.CreateSpeciesBreedValue(
                    speciesBreedResult.Value.speciesId,
                    speciesBreedResult.Value.breedId)
                : _speciesContract.CreateSpeciesBreedValue(SpeciesId.Empty(), BreedId.Empty());

            var petDescription =
                DescriptionValueObject.Create(command.Description).Value;

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
                [requisite.Value],
                birthday,
                createDate,
                command.IsNeutered,
                command.IsVaccinated,
                command.HelpStatus,
                command.Weight,
                command.Height);
        }

        private Result<(SpeciesId speciesId, BreedId breedId)> GetSpeciesAndBreed(
            AddPetCommand command)
        {
            var speciesRequestResult = _speciesContract.GetSpeciesDtos()
                .FirstOrDefault(s => s.Id == command.SpeciesId);

            if (speciesRequestResult != null)
            {
                var speciesId = SpeciesId.Create(speciesRequestResult.Id);

                var breedResult = speciesRequestResult.Breeds
                    .FirstOrDefault(b => b.Id == command.BreedId);

                if (breedResult == null)
                {
                    return Errors.General.NotFound(command.BreedId);
                }

                var breedId = BreedId.Create(command.BreedId);

                return (speciesId, breedId);
            }
            return Errors.General.NotFound(command.SpeciesId);
        }
    }
}
