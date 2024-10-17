using PetHome.Species.Application.SpeciesManagement.Queries.GetSpecies;

namespace PetHome.Species.Application.Contracts
{
    public record GetSpeciesWithPaginationRequest(int Page, int PageSize, string? SortDirection)
    {
        public GetSpeciesWithPaginationQuery ToQuery() => new(Page, PageSize, SortDirection);
    }
}
