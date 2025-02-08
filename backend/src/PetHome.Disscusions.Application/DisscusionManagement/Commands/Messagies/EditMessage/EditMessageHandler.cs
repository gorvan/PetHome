using FluentValidation;
using Microsoft.Extensions.Logging;
using PetHome.Shared.Core.Abstractions;
using PetHome.Shared.Core.Extensions;
using PetHome.Shared.Core.Shared;
using PetHome.Shared.Core.Shared.IDs;

namespace PetHome.Disscusions.Application.DisscusionManagement.Commands.Messagies.EditMessage;
public class EditMessageHandler : ICommandHandler<Guid, EditMessageCommand>
{
    private readonly IDisscusionRepositiory _disscusionRepository;
    private readonly ILogger<EditMessageHandler> _logger;
    private readonly IValidator<EditMessageCommand> _validator;

    public EditMessageHandler(
        IDisscusionRepositiory disscusionRepository,
        ILogger<EditMessageHandler> logger,
        IValidator<EditMessageCommand> validator)
    {
        _disscusionRepository = disscusionRepository;
        _logger = logger;
        _validator = validator;
    }

    public async Task<Result<Guid>> Execute(EditMessageCommand command, CancellationToken token)
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

        if (!disscusionResult.Value.Users.Any(u => u == command.UserId))
        {
            return Errors.General.NotFound(command.UserId);
        }

        var message = disscusionResult.Value.Messages
            .FirstOrDefault(m => m.MessageId.Id == command.MessageId);

        if (message is null)
        {
            return Errors.General.NotFound(command.MessageId);
        }

        message.Edit(command.NewMessage);

        await _disscusionRepository.Update(disscusionResult.Value, token);

        _logger.LogInformation("Edit comment, Id: {id}", command.MessageId);

        return message.MessageId.Id;
    }
}
