using PetHome.Shared.Core.Shared;
using PetHome.Shared.Core.Shared.IDs;

namespace PetHome.Disscusions.Domain;

public class Disscusion
{
    private Disscusion(
        DisscusionId disscusionId,
        Guid relationId,
        IReadOnlyList<Guid> users)
    {
        DisscusionId = disscusionId;
        RelationId = relationId;
        Users = users;
        State = DisscusionState.Opened;
    }

    public DisscusionId DisscusionId { get; init; } = default!;
    public Guid RelationId { get; init; } = default!;
    public IReadOnlyList<Guid> Users { get; private set; } = [];
    public List<Message> Messages { get; private set; } = [];
    public DisscusionState State { get; private set; }

    public static Result<Disscusion> Create(
        DisscusionId disscusionId,
        Guid relationId,
        List<Guid> users)
    {
        if (users.Count < 2)
        {
            return Errors.General.ValueIsInvalid("Users less than 2");
        }

        var disscusion = new Disscusion(disscusionId, relationId, users);

        return disscusion;
    }

    public Guid AddComment(Message message)
    {  
        Messages.Add(message);

        return message.MessageId.Id;
    }

    public Guid DeleteComment(Message message)
    {
        Messages.Remove(message);

        return message.MessageId.Id;
    }
   
    public void CloseDisscusion()
    {
        State = DisscusionState.Closed;
    }
}
