using PetHome.Accounts.Domain;
using PetHome.Shared.Core.Shared;
using PetHome.Shared.Core.Shared.IDs;

namespace PetHome.Disscusions.Domain;

public class Disscusion
{
    private Disscusion(
        DisscusionId disscusionId,
        Guid relationId,
        List<User> users)
    {
        DisscusionId = disscusionId;
        RelationId = relationId;
        Users = users;
        State = DisscusionState.Opened;
    }

    public DisscusionId DisscusionId { get; init; } = default!;
    public Guid RelationId { get; init; } = default!;
    public IReadOnlyList<User> Users { get; private set; } = [];
    public List<Message> Messages { get; private set; } = [];
    public DisscusionState State { get; private set; }

    public static Result<Disscusion> Create(
        DisscusionId disscusionId,
        Guid relationId,
        List<User> users)
    {
        if (users.Count < 2)
        {
            return Errors.General.ValueIsInvalid("Users less than 2");
        }

        var disscusion = new Disscusion(disscusionId, relationId, users);

        return disscusion;
    }

    public Result AddComment(string text, Guid userId)
    {
        if (!Users.Any(u => u.Id == userId))
        {
            return Errors.General.NotFound(userId);
        }

        var messageId = MessageId.NewMessageId();

        var messageResult = Message.Create(
            messageId,
            text,
            DateTime.UtcNow,
            false,
            userId);

        if (messageResult.IsFailure)
        {
            return messageResult.Error;
        }

        Messages.Add(messageResult.Value);

        return Result.Success();
    }

    public Result DeleteComment(Guid userId, MessageId messageId)
    {
        if (!Users.Any(u => u.Id == userId))
        {
            return Errors.General.NotFound(userId);
        }

        var message = Messages.FirstOrDefault(m => m.MessageId == messageId);

        if (message == null)
        {
            return Errors.General.NotFound(messageId);
        }

        Messages.Remove(message);

        return Result.Success();
    }

    public Result EditComment(Guid userId, MessageId messageId, string text)
    {
        if (!Users.Any(u => u.Id == userId))
        {
            return Errors.General.NotFound(userId);
        }

        var message = Messages.FirstOrDefault(m => m.MessageId == messageId);

        if (message == null)
        {
            return Errors.General.NotFound(messageId);
        }

        var editedMessageId = MessageId.Create(message.MessageId.Id);

        var newMessageResult = Message.Create(
            editedMessageId,
            text,
            message.CreatedAt,
            true,
            userId);

        if (newMessageResult.IsFailure)
        {
            return newMessageResult.Error;
        }

        Messages.Remove(message);

        Messages.Add(newMessageResult.Value);

        return Result.Success();
    }

    public void CloseDisscusion()
    {
        State = DisscusionState.Closed;
    }
}
