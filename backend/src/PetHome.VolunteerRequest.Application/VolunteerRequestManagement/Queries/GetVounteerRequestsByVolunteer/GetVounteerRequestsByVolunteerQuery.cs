using PetHome.Shared.Core.Abstractions;
using PetHome.Shared.Core.Shared;

namespace PetHome.VolunteerRequests.Application.VolunteerRequestManagement.Queries.GetVounteerRequestsByVolunteer;
public record GetVounteerRequestsByVolunteerQuery(
    Guid VolunteerId,
    int Page,
    int PageSize,
    RequestStatus Status) : IQuery;
