using CSharpFunctionalExtensions;

namespace PetHome.VolunteerRequests.Domain.ValueObjects;

public class DisscusionId : ValueObject
{
    private DisscusionId(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; }

    public static DisscusionId NewDisscusionId() => new(Guid.NewGuid());
    public static DisscusionId Empty() => new(Guid.Empty);
    public static DisscusionId Create(Guid id) => new(id);

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Id;
    }

    public static implicit operator Guid(DisscusionId disscusionId)
    {
        if (disscusionId is null)
        {
            throw new ArgumentNullException();
        }

        return disscusionId.Id;
    }
}
