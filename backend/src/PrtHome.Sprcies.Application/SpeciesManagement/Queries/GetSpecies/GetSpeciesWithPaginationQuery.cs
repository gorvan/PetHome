using PetHome.Shared.Core.Abstractions;

namespace PetHome.Species.Application.SpeciesManagement.Queries.GetSpecies
{
    public record GetSpeciesWithPaginationQuery(
        int Page,
        int PageSize,
        string? SortDirection) : IQuery;
}
