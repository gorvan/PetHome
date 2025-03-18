using PetHome.Shared.Core.Abstractions;
using PetHome.Shared.Core.Shared;

namespace PetHome.VolunteerRequests.Application.VolunteerRequestManagement.Queries.GetReviewedVolunteerRequestsByAdmin;
public record GetReviewedVolunteerRequestsByAdminQuery(
    Guid AdminId,
    int Page,
    int PageSize,
    RequestStatus Status) : IQuery;
