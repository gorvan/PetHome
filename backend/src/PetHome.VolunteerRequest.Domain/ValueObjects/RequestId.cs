using CSharpFunctionalExtensions;
namespace PetHome.VolunteerRequests.Domain.ValueObjects;

public class RequestId : ValueObject
{
    private RequestId(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; }

    public static RequestId NewRequestId() => new(Guid.NewGuid());
    public static RequestId Empty() => new(Guid.Empty);
    public static RequestId Create(Guid id) => new(id);

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Id;
    }

    public static implicit operator Guid(RequestId userId)
    {
        if (userId is null)
        {
            throw new ArgumentNullException();
        }

        return userId.Id;
    }
}
