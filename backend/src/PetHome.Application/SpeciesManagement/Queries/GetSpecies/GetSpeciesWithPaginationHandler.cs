using PetHome.Application.Abstractions;
using PetHome.Application.Database;
using PetHome.Application.Dtos;
using PetHome.Application.Extensions;
using PetHome.Application.Models;
using PetHome.Domain.Shared;

namespace PetHome.Application.SpeciesManagement.Queries.GetSpecies
{
    public class GetSpeciesWithPaginationHandler : 
        IQueryHandler<Result<PagedList<SpeciesDto>>, GetSpeciesWithPaginationQuery>
    {
        private readonly IReadDbContext _readDbContext;

        public GetSpeciesWithPaginationHandler(IReadDbContext readDbContext)
        {
            _readDbContext = readDbContext;
        }

        public async Task<Result<PagedList<SpeciesDto>>> Execute(
            GetSpeciesWithPaginationQuery query,
            CancellationToken token)
        {
            var speciesQuery = _readDbContext.Species;

            speciesQuery = query.SortDirection?.ToLower() == Constants.SORT_DESCENDING
                ? speciesQuery.OrderByDescending(s => s.Name)
                : speciesQuery.OrderBy(s => s.Name);

            return await speciesQuery
                    .ToPagedList(query.Page, query.PageSize, token);
        }
    }
}
