using PetHome.Application.Abstractions;

namespace PetHome.Application.VolunteersManagement.Queries.GetVolunteersWithPagination
{
    public record GetVolunteersWithPaginationFilteredQuery(
        int? Experience,
        int Page, 
        int PageSize) : IQuery;    
}
