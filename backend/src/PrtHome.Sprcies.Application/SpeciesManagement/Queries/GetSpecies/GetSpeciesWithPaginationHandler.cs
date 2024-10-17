using PetHome.Shared.Core.Abstractions;
using PetHome.Shared.Core.Dtos;
using PetHome.Shared.Core.Extensions;
using PetHome.Shared.Core.Models;
using PetHome.Shared.Core.Shared;

namespace PetHome.Species.Application.SpeciesManagement.Queries.GetSpecies
{
    public class GetSpeciesWithPaginationHandler :
        IQueryHandler<Result<PagedList<SpeciesDto>>, GetSpeciesWithPaginationQuery>
    {
        private readonly IReadDbContextSpecies _readDbContext;

        public GetSpeciesWithPaginationHandler(IReadDbContextSpecies readDbContext)
        {
            _readDbContext = readDbContext;
        }

        public async Task<Result<PagedList<SpeciesDto>>> Execute(
            GetSpeciesWithPaginationQuery query,
            CancellationToken token)
        {
            var speciesQuery = _readDbContext.Species;

            speciesQuery = query.SortDirection?.ToLower() == Shared.Core.Constants.SORT_DESCENDING
                ? speciesQuery.OrderByDescending(s => s.Name)
                : speciesQuery.OrderBy(s => s.Name);

            return await speciesQuery
                    .ToPagedList(query.Page, query.PageSize, token);
        }
    }
}
