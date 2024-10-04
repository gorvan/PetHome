using PetHome.Application.Abstractions;

namespace PetHome.Application.VolunteersManagement.Queries.GetVolunteersWithPagination
{
    public record GetVolunteersWithPaginationFilteredQuery(
        int? Experience,
        string? SortBy,
        string? SortDirection,
        int Page, 
        int PageSize) : IQuery;    
}
