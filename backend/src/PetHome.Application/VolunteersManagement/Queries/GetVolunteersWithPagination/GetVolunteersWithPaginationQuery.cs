using PetHome.Application.Abstractions;

namespace PetHome.Application.VolunteersManagement.Queries.GetVolunteersWithPagination
{
    public record GetVolunteersWithPaginationQuery(
        int Page, 
        int PageSize) : IQuery;    
}
