using FluentValidation;
using Microsoft.Extensions.Logging;
using PetHome.Disscusions.Domain;
using PetHome.Shared.Core.Abstractions;
using PetHome.Shared.Core.Extensions;
using PetHome.Shared.Core.Shared;
using PetHome.Shared.Core.Shared.IDs;

namespace PetHome.Disscusions.Application.DisscusionManagement.Commands.Messagies.AddMessage;
public class AddMessageHandler : ICommandHandler<Guid, AddMessageCommand>
{
    private readonly IDisscusionRepositiory _disscusionRepository;
    private readonly ILogger<AddMessageHandler> _logger;
    private readonly IValidator<AddMessageCommand> _validator;

    public AddMessageHandler(
        IDisscusionRepositiory disscusionRepository,
        ILogger<AddMessageHandler> logger,
        IValidator<AddMessageCommand> validator)
    {
        _disscusionRepository = disscusionRepository;
        _logger = logger;
        _validator = validator;
    }

    public async Task<Result<Guid>> Execute(AddMessageCommand command, CancellationToken token)
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

        var messageId = MessageId.NewMessageId();

        var messageResult = Message.Create(
            messageId,
            command.Message,
            DateTime.UtcNow,
            false,
            command.UserId);

        if (messageResult.IsFailure)
        {
            return messageResult.Error;
        }

        var addCommentResult = disscusionResult.Value.AddComment(messageResult.Value);

        await _disscusionRepository.Update(disscusionResult.Value, token);

        _logger.LogInformation("Add new comment, Id: {id}", addCommentResult);

        return addCommentResult;
    }
}
