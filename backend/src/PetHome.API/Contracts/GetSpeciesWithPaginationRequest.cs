using PetHome.Application.SpeciesManagement.Queries.GetSpecies;

namespace PetHome.API.Contracts
{
    public record GetSpeciesWithPaginationRequest(int Page, int PageSize, string? SortDirection)
    {
        public GetSpeciesWithPaginationQuery ToQuery() => new(Page, PageSize, SortDirection);
    }
}
