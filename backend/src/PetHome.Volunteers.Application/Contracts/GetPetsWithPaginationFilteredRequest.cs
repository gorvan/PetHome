using PetHome.Volunteers.Application.VolunteersManagement.Queries.GetAllPets;

namespace PetHome.Volunteers.Application.Contracts
{
    public record GetPetsWithPaginationFilteredRequest(
        string? FilterBy,
        object? FilterValue,
        string? SortBy,
        string? SortDirection,
        int Page,
        int PageSize)
    {
        public GetPetsWithPaginationFilteredQuery ToQuery() =>
            new(FilterBy,
                FilterValue,
                SortBy,
                SortDirection,
                Page,
                PageSize);
    }
}
