using PetHome.Shared.Core.Shared;
using PetHome.Shared.Core.Shared.IDs;

namespace PetHome.Disscusions.Domain;

public class Message
{
    public MessageId MessageId { get; set; }
    public string Text { get; private set; }
    public DateTime CreatedAt { get; }
    public bool IsEdited { get; private set; }
    public Guid UserId { get; }

    private Message(
        MessageId messageId,
        string text,
        DateTime createdAt,
        bool isEdited,
        Guid userId)
    {
        MessageId = messageId;
        Text = text;
        CreatedAt = createdAt;
        IsEdited = isEdited;
        UserId = userId;
    }

    public static Result<Message> Create(
        MessageId messageId,
        string text,
        DateTime createdAt,
        bool isEdited,
        Guid userId)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            return Errors.General.ValueIsRequeired(nameof(text));
        }

        return new Message(
            messageId,
            text,
            createdAt,
            isEdited,
            userId);
    }
}
