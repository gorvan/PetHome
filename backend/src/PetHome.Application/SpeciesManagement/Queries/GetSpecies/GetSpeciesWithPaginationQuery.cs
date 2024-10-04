using PetHome.Application.Abstractions;

namespace PetHome.Application.SpeciesManagement.Queries.GetSpecies
{
    public record GetSpeciesWithPaginationQuery(
        int Page, 
        int PageSize, 
        string? SortDirection) : IQuery;    
}
