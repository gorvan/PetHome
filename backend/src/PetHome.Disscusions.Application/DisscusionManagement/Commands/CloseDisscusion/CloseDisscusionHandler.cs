using FluentValidation;
using Microsoft.Extensions.Logging;
using PetHome.Shared.Core.Abstractions;
using PetHome.Shared.Core.Extensions;
using PetHome.Shared.Core.Shared;
using PetHome.Shared.Core.Shared.IDs;

namespace PetHome.Disscusions.Application.DisscusionManagement.Commands.CloseDisscusion;
public class CloseDisscusionHandler : ICommandHandler<Guid, CloseDisscusionCommand>
{
    private readonly IDisscusionRepositiory _disscusionRepository;
    private readonly ILogger<CloseDisscusionCommand> _logger;
    private readonly IValidator<CloseDisscusionCommand> _validator;

    public CloseDisscusionHandler(
        IDisscusionRepositiory disscusionRepository,
        ILogger<CloseDisscusionCommand> logger,
        IValidator<CloseDisscusionCommand> validator)
    {
        _disscusionRepository = disscusionRepository;
        _logger = logger;
        _validator = validator;
    }

    public async Task<Result<Guid>> Execute(
        CloseDisscusionCommand command, 
        CancellationToken token)
    {
        var validationResult = await _validator.ValidateAsync(command, token);
        if (validationResult.IsValid == false)
        {
            return validationResult.ToErrorList();
        }

        var disscusionId = DisscusionId.Create(command.DisscusionId);

        var disscusionResult = await _disscusionRepository.GetById(disscusionId, token);

        if (disscusionResult.IsFailure)
        {
            return disscusionResult.Error;
        }

        disscusionResult.Value.CloseDisscusion();

        var updateResult = await _disscusionRepository.Update(disscusionResult.Value, token);

        if (updateResult.IsFailure)
        {
            return updateResult.Error;
        }

        return updateResult.Value;
    }
}
