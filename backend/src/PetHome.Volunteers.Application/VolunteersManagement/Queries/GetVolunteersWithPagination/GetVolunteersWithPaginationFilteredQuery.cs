using PetHome.Shared.Core.Abstractions;

namespace PetHome.Volunteers.Application.VolunteersManagement.Queries.GetVolunteersWithPagination
{
    public record GetVolunteersWithPaginationFilteredQuery(
        int? Experience,
        string? SortBy,
        string? SortDirection,
        int Page,
        int PageSize) : IQuery;
}
