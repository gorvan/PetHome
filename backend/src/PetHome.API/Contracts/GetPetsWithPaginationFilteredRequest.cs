using PetHome.Application.VolunteersManagement.Queries.GetAllPets;

namespace PetHome.API.Contracts
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
