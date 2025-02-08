using FluentValidation;
using Microsoft.Extensions.Logging;
using PetHome.Disscusions.Domain;
using PetHome.Shared.Core.Abstractions;
using PetHome.Shared.Core.Extensions;
using PetHome.Shared.Core.Shared;
using PetHome.Shared.Core.Shared.IDs;

namespace PetHome.Disscusions.Application.DisscusionManagement.Commands.CreateDisscusion;
public class CreateDisscusionHandler : ICommandHandler<Guid, CreateDisscusionCommand>
{
    private readonly IDisscusionRepositiory _disscusionRepository;
    private readonly ILogger<CreateDisscusionHandler> _logger;
    private readonly IValidator<CreateDisscusionCommand> _validator;

    public CreateDisscusionHandler(
        IDisscusionRepositiory disscusionRepository,
        ILogger<CreateDisscusionHandler> logger,
        IValidator<CreateDisscusionCommand> validator)
    {
        _disscusionRepository = disscusionRepository;
        _logger = logger;
        _validator = validator;
    }

    public async Task<Result<Guid>> Execute(CreateDisscusionCommand command, CancellationToken token)
    {
        var validationResult = await _validator.ValidateAsync(command, token);
        if (validationResult.IsValid == false)
        {
            return validationResult.ToErrorList();
        }

        var disscusionId = DisscusionId.NewDisscusionId();
        
        var disscusion = Disscusion.Create(disscusionId, command.RelationId, command.UserIds).Value;
               
        await _disscusionRepository.Add(disscusion, token);

        return (Guid)disscusion.DisscusionId;
    }
}
