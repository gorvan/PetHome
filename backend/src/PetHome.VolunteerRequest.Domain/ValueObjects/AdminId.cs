using CSharpFunctionalExtensions;

namespace PetHome.VolunteerRequests.Domain.ValueObjects;

public class AdminId : ValueObject
{
    private AdminId(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; }

    public static AdminId NewAdminId() => new(Guid.NewGuid());
    public static AdminId Empty() => new(Guid.Empty);
    public static AdminId Create(Guid id) => new(id);

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Id;
    }

    public static implicit operator Guid(AdminId adminId)
    {
        if (adminId is null)
        {
            throw new ArgumentNullException();
        }

        return adminId.Id;
    }
}
