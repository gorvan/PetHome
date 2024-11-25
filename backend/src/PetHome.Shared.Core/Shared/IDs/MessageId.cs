using CSharpFunctionalExtensions;

namespace PetHome.Shared.Core.Shared.IDs;

public class MessageId : ValueObject
{
    private MessageId(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; }

    public static MessageId NewMessageId() => new(Guid.NewGuid());
    public static MessageId Empty() => new(Guid.Empty);
    public static MessageId Create(Guid id) => new(id);

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Id;
    }

    public static implicit operator Guid(MessageId messageId)
    {
        if (messageId is null)
        {
            throw new ArgumentNullException();
        }

        return messageId.Id;
    }
}

