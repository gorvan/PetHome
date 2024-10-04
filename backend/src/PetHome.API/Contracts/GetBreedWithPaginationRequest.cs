using PetHome.Application.SpeciesManagement.Queries.GetBreeds;

namespace PetHome.API.Contracts
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
