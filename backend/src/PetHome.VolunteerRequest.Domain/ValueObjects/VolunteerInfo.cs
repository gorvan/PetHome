using PetHome.Shared.Core.Shared;

namespace PetHome.VolunteerRequests.Domain.ValueObjects;

public record VolunteerInfo
{
    private VolunteerInfo() { }
    private VolunteerInfo(
        FullName fullName,
        Email email,
        DescriptionValueObject description,
        Phone phone)
    {
        FullName = fullName;
        Email = email;
        Description = description;
        Phone = phone;
    }

    public FullName FullName { get; }
    public Email Email { get; }
    public DescriptionValueObject Description { get; }
    public Phone Phone { get; }

    public static VolunteerInfo Create(
        FullName fullName,
        Email email,
        DescriptionValueObject description,
        Phone phone)
    {
        return new VolunteerInfo(
            fullName,
            email,
            description,
            phone);
    }
}
