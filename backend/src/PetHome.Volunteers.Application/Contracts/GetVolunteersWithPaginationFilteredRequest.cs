using PetHome.Volunteers.Application.VolunteersManagement.Queries.GetVolunteersWithPagination;

namespace PetHome.Volunteers.Application.Contracts
{
    public record GetVolunteersWithPaginationFilteredRequest(
        int? Experience,
        string? SortBy,
        string? SortDirection,
        int Page,
        int PageSize)
    {
        public GetVolunteersWithPaginationFilteredQuery ToQuery() =>
            new(Experience, SortBy, SortDirection, Page, PageSize);
    }
}
