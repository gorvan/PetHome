using PetHome.Application.Abstractions;

namespace PetHome.Application.SpeciesManagement.Queries.GetBreeds
{
    public record GetBreedsWithPaginationQuery(
        Guid SpeciesId,
        int Page, 
        int PageSize, 
        string? SortDirection) : IQuery;  
}
