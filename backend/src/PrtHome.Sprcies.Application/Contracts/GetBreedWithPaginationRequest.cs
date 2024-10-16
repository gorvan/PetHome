using PetHome.Species.Application.SpeciesManagement.Queries.GetBreeds;

namespace PetHome.Species.Application.Contracts
{
    public record GetBreedWithPaginationRequest(
        int Page,
        int PageSize,
        string? SortDirection)
    {
        public GetBreedsWithPaginationQuery ToQuery(Guid speciesId) =>
            new(speciesId, Page, PageSize, SortDirection);
    }
}
