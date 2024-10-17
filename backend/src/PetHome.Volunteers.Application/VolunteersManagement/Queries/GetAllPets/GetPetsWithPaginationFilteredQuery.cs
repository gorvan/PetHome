using PetHome.Shared.Core.Abstractions;

namespace PetHome.Volunteers.Application.VolunteersManagement.Queries.GetAllPets
{
    public record GetPetsWithPaginationFilteredQuery(
        string? FilterBy,
        object? FilterValue,
        string? SortBy,
        string? SortDirection,
        int Page,
        int PageSize) : IQuery;
}
