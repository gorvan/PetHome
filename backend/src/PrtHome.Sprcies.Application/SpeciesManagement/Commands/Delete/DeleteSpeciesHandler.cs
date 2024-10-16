using FluentValidation;
using Microsoft.Extensions.Logging;
using PetHome.Shared.Core.Abstractions;
using PetHome.Shared.Core.Extensions;
using PetHome.Shared.Core.Shared;
using PetHome.Shared.Core.Shared.IDs;
using PetHome.Volunteers.Contracts;

namespace PetHome.Species.Application.SpeciesManagement.Commands.Delete
{
    public class DeleteSpeciesHandler : ICommandHandler<Guid, DeleteSpeciesCommand>
    {
        private readonly ISpeciesRepository _speciesRepository;
        private readonly IVolunteersContract _volunteersContract;
        private readonly ILogger<DeleteSpeciesHandler> _logger;
        private readonly IValidator<DeleteSpeciesCommand> _validator;

        public DeleteSpeciesHandler(
            ISpeciesRepository speciesRepository,           
            IVolunteersContract volunteersContract,
            ILogger<DeleteSpeciesHandler> logger,
            IValidator<DeleteSpeciesCommand> validator)
        {
            _speciesRepository = speciesRepository;
            _volunteersContract = volunteersContract;
            _logger = logger;
            _validator = validator;
        }

        public async Task<Result<Guid>> Execute(
            DeleteSpeciesCommand command,            
            CancellationToken token)
        {
            var validationResult = await _validator
                .ValidateAsync(command, token);

            if (validationResult.IsValid == false)
            {
                return validationResult.ToErrorList();
            }

            var speciesId = SpeciesId.Create(command.SpeciesId);

            var speciesResult = await _speciesRepository
                .GetById(speciesId, token);

            if (speciesResult.IsFailure)
            {
                return Errors.General.NotFound(speciesId.Id);
            }

            var petsWithSpecies = _volunteersContract.GetPetDtos()
                .Where(p => p.SpeciesId == speciesId.Id).ToList();

            if (petsWithSpecies.Count > 0)
            {
                return Errors.General.ValueIsUsed(speciesId.Id);
            }

            var result = await _speciesRepository
                .Delete(speciesResult.Value, token);

            _logger.LogInformation("Delete species with id {speciesId}", speciesId);

            return speciesResult.Value.Id.Id;
        }
    }
}
