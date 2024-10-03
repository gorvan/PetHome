using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PetHome.Application.Abstractions;
using PetHome.Application.Database;
using PetHome.Application.Extensions;
using PetHome.Application.SpeciesManagement;
using PetHome.Domain.PetManadgement.Entities;
using PetHome.Domain.PetManadgement.ValueObjects;
using PetHome.Domain.Shared;
using PetHome.Domain.Shared.IDs;
using PetHome.Domain.SpeciesManagement.AggregateRoot;
using PetHome.Domain.SpeciesManagement.Entities;
using PetHome.Domain.SpeciesManagement.ValueObjects;

namespace PetHome.Application.VolunteersManagement.Commands.PetManagement.AddPet
{
    public class AddPetHandler : ICommandHandler<Guid, AddPetCommand>
    {
        private readonly IVolunteerRepository _volunteerRepository;
        private readonly ISpeciesRepository _speciesRepository;
        private readonly IReadDbContext _readDbContext;
        private readonly ILogger<AddPetHandler> _logger;
        private readonly IValidator<AddPetCommand> _validator;

        public AddPetHandler(
            IVolunteerRepository volunteerRepository,
            ISpeciesRepository speciesRepository,
            IReadDbContext readDbContext,
            ILogger<AddPetHandler> logger,
            IValidator<AddPetCommand> validator)
        {
            _volunteerRepository = volunteerRepository;
            _speciesRepository = speciesRepository;
            _readDbContext = readDbContext;
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

            var petResult = await CreatePet(command, token);

            if (petResult.IsFailure)
            {
                return petResult.Error;
            }

            volunteerResult.Value.AddPet(petResult.Value);

            await _volunteerRepository.Update(volunteerResult.Value, token);

            _logger.LogInformation("Add new pet, Id: {petId}", petResult.Value.Id);

            return petResult.Value.Id.Id;
        }

        private async Task<Result<Pet>> CreatePet(
            AddPetCommand command, 
            CancellationToken token)
        {
            var petId = PetId.NewPetId();

            var petNickName =
                PetNickname.Create(command.Nickname).Value;

            var speciesBreedResult = await GetSpeciesAndBreed(command, token);

            if (speciesBreedResult.IsFailure)
            {
                return speciesBreedResult.Error;
            }

            var speciesBreedValue = new SpeciesBreedValue(
                speciesBreedResult.Value.speciesId, 
                speciesBreedResult.Value.breedId);

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

        private async Task<Result<(SpeciesId speciesId, BreedId breedId)>> GetSpeciesAndBreed(
            AddPetCommand command,
            CancellationToken token)
        {
            SpeciesId speciesId;
            BreedId breedId;

            var speciesRequestResult = await _readDbContext.Species
                .FirstOrDefaultAsync(s => s.Name == command.Species, token);

            if (speciesRequestResult != null)
            {
                speciesId = SpeciesId.Create(speciesRequestResult.Id);

                var breedResult = speciesRequestResult.Breeds
                    .FirstOrDefault(b => b.Name == command.Breed);

                if (breedResult == null)
                {
                    var speciesValue = await _speciesRepository
                        .GetById(speciesId, token);

                    if (speciesValue.IsFailure)
                    {
                        return Errors.General.NotFound(speciesId.Id);
                    }

                    breedId = BreedId.NewBreedId();
                    var resultBreed = Breed.Create(breedId, command.Breed);

                    if (resultBreed.IsFailure)
                    {
                        return resultBreed.Error;
                    }

                    speciesValue.Value.AddBreed(resultBreed.Value);

                    var updateResult = await _speciesRepository
                        .Update(speciesValue.Value, token);
                }
                else
                {
                    breedId = BreedId.Create(breedResult.Id);
                }
            }
            else
            {
                breedId = BreedId.NewBreedId();
                var breedValue = Breed.Create(breedId, command.Breed);

                speciesId = SpeciesId.NewSpeciesId();
                var speciesValue = Species.Create(
                    speciesId, 
                    command.Species, 
                    [breedValue.Value]);

                var requestResult = await _speciesRepository
                    .Add(speciesValue.Value, token);
            }

            return (speciesId, breedId);
        }
    }
}
