using PetHome.Shared.Core.Abstractions;

namespace PetHome.VolunteerRequests.Application.VolunteerRequestManagement.Queries.GetNotReviewedVolunteerRequests;
public record GetNotReviewedVolunteerRequestsQuery(
    Guid AdminId, 
    int Page,
    int PageSize) : IQuery;
