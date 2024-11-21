using PetHome.Shared.Core.Shared;

namespace PetHome.VolunteerRequests.Domain.ValueObjects;

public record VolunteerInfo(
     FullName FullName,
     Email Email,
     DescriptionValueObject Description,
     Phone Phone);
