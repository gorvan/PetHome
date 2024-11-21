using CSharpFunctionalExtensions;

namespace PetHome.VolunteerRequests.Domain.ValueObjects;

public class UserId : ValueObject
{
    private UserId(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; }

    public static UserId NewUserId() => new(Guid.NewGuid());
    public static UserId Empty() => new(Guid.Empty);
    public static UserId Create(Guid id) => new(id);

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Id;
    }

    public static implicit operator Guid(UserId userId)
    {
        if (userId is null)
        {
            throw new ArgumentNullException();
        }

        return userId.Id;
    }
}