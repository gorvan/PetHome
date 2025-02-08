using FluentValidation;
using Microsoft.Extensions.Logging;
using PetHome.Shared.Core.Abstractions;
using PetHome.Shared.Core.Extensions;
using PetHome.Shared.Core.Shared;
using PetHome.Shared.Core.Shared.IDs;

namespace PetHome.Disscusions.Application.DisscusionManagement.Commands.Messagies.DeleteMessage;
public class DeleteMessageHandler : ICommandHandler<Guid, DeleteMessageCommand>
{
    private readonly IDisscusionRepositiory _disscusionRepository;
    private readonly ILogger<DeleteMessageHandler> _logger;
    private readonly IValidator<DeleteMessageCommand> _validator;

    public DeleteMessageHandler(
        IDisscusionRepositiory disscusionRepository,
        ILogger<DeleteMessageHandler> logger,
        IValidator<DeleteMessageCommand> validator)
    {
        _disscusionRepository = disscusionRepository;
        _logger = logger;
        _validator = validator;
    }

    public async Task<Result<Guid>> Execute(DeleteMessageCommand command, CancellationToken token)
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

        disscusionResult.Value.DeleteComment(message);

        await _disscusionRepository.Update(disscusionResult.Value, token);

        _logger.LogInformation("Edit comment, Id: {id}", command.MessageId);

        return message.MessageId.Id;
    }
}
