using PetHome.Shared.Core.Abstractions;

namespace PetHome.Species.Application.SpeciesManagement.Queries.GetBreeds
{
    public record GetBreedsWithPaginationQuery(
        Guid SpeciesId,
        int Page,
        int PageSize,
        string? SortDirection) : IQuery;
}
