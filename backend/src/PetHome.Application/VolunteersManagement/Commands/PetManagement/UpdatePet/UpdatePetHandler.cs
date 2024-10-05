using FluentValidation;
using Microsoft.Extensions.Logging;
using PetHome.Application.Abstractions;
using PetHome.Application.Database;
using PetHome.Application.Extensions;
using PetHome.Domain.PetManadgement.Entities;
using PetHome.Domain.PetManadgement.ValueObjects;
using PetHome.Domain.Shared;
using PetHome.Domain.Shared.IDs;
using PetHome.Domain.SpeciesManagement.AggregateRoot;

namespace PetHome.Application.VolunteersManagement.Commands.PetManagement.UpdatePet
{
    public class UpdatePetHandler : ICommandHandler<Guid, UpdatePetCommand>
    {
        private readonly IVolunteerRepository _volunteerRepository;
        private readonly IReadDbContext _readDbContext;
        private readonly ILogger<UpdatePetHandler> _logger;
        private readonly IValidator<UpdatePetCommand> _validator;

        public UpdatePetHandler(
            IVolunteerRepository volunteerRepository,
            IReadDbContext readDbContext,
            ILogger<UpdatePetHandler> logger,
            IValidator<UpdatePetCommand> validator)
        {
            _volunteerRepository = volunteerRepository;
            _readDbContext = readDbContext;
            _logger = logger;
            _validator = validator;
        }

        public async Task<Result<Guid>> Execute(UpdatePetCommand command, CancellationToken token)
        {
            var validationResult = await _validator.ValidateAsync(command, token);
            if (validationResult.IsValid == false)
            {
                return validationResult.ToErrorList();
            }

            var volunteerId = VolunteerId.Create(command.VolunteerId);
            var volunteerResult =
                await _volunteerRepository.GetById(volunteerId, token);

            if (volunteerResult.IsFailure)
            {
                return volunteerResult.Error;
            }

            var pet = volunteerResult.Value.Pets
                .FirstOrDefault(p => p.Id.Id == command.PetId);

            if (pet == null)
            {
                return Errors.General.NotFound(command.PetId);
            }

            var petResult = UpdatePet(pet, command);

            var result =
                await _volunteerRepository.Update(volunteerResult.Value, token);

            _logger.LogInformation("Updated pet with id {pet.Id.Id}", pet.Id.Id);

            return result;
        }

        private Result UpdatePet(Pet pet, UpdatePetCommand command)
        {
            var petNickName =
                PetNickname.Create(command.Nickname).Value;

            var speciesBreedResult = GetSpeciesAndBreed(command);

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

            pet.Update(
            petNickName,
            speciesBreedValue,
            petDescription,
            petColor,
            healthInfo,
            address,
            phone,
            [requisite.Value],
            birthday,
            command.IsNeutered,
            command.IsVaccinated,
            command.HelpStatus,
            command.Weight,
            command.Height);

            return Result.Success();
        }

        private Result<(SpeciesId speciesId, BreedId breedId)> GetSpeciesAndBreed(
            UpdatePetCommand command)
        {
            var speciesRequestResult = _readDbContext.Species
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
