using FluentValidation;
using Microsoft.Extensions.Logging;
using PetHome.Shared.Core.Abstractions;
using PetHome.Shared.Core.Extensions;
using PetHome.Shared.Core.Shared;
using PetHome.Shared.Core.Shared.IDs;
using PetHome.Species.Contracts;

namespace PetHome.Volunteers.Application.VolunteersManagement.Commands.PetManagement.UpdatePet
{
    public class UpdatePetHandler : ICommandHandler<Guid, UpdatePetCommand>
    {
        private readonly IVolunteerRepository _volunteerRepository;
        private readonly ISpeciesContract _speciesContract;
        private readonly ILogger<UpdatePetHandler> _logger;
        private readonly IValidator<UpdatePetCommand> _validator;

        public UpdatePetHandler(
            IVolunteerRepository volunteerRepository,
            ISpeciesContract speciesContract,
            ILogger<UpdatePetHandler> logger,
            IValidator<UpdatePetCommand> validator)
        {
            _volunteerRepository = volunteerRepository;
            _speciesContract = speciesContract;
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
            var volunteerResult = await _volunteerRepository.GetById(volunteerId, token);

            if (volunteerResult.IsFailure)
            {
                return volunteerResult.Error;
            }

            var speciesBreedResult = GetSpeciesAndBreed(command);

            if (speciesBreedResult.IsFailure)
            {
                return speciesBreedResult.Error;
            }

            var speciesBreedValue = new SpeciesBreedValue(
               speciesBreedResult.Value.speciesId,
               speciesBreedResult.Value.breedId);

            var petResult = volunteerResult.Value.UpdatePet(
                command.PetId,
                command.Nickname,
                speciesBreedValue,
                command.Description,
                command.Color,
                command.Health,
                command.Address.City,
                command.Address.Street,
                command.Address.HouseNumber,
                command.Address.AppartmentNumber,
                command.Phone,
                command.Requisites.Name,
                command.Requisites.Description,
                command.BirthDay,
                command.IsNeutered,
                command.IsVaccinated,
                command.HelpStatus,
                command.Weight,
                command.Height);
            if (petResult.IsFailure)
            {
                return petResult.Error;
            }

            var result =
                await _volunteerRepository.Update(volunteerResult.Value, token);

            _logger.LogInformation("Updated pet with id {petId}", command.PetId);

            return result;
        }

        private Result<(SpeciesId speciesId, BreedId breedId)> GetSpeciesAndBreed(
            UpdatePetCommand command)
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
