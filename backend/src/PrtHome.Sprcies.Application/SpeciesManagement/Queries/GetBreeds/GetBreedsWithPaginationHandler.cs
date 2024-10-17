using Microsoft.EntityFrameworkCore;
using PetHome.Shared.Core.Abstractions;
using PetHome.Shared.Core.Dtos;
using PetHome.Shared.Core.Extensions;
using PetHome.Shared.Core.Models;
using PetHome.Shared.Core.Shared;
using Constants = PetHome.Shared.Core.Constants;

namespace PetHome.Species.Application.SpeciesManagement.Queries.GetBreeds
{
    public class GetBreedsWithPaginationHandler :
        IQueryHandler<Result<PagedList<BreedDto>>, GetBreedsWithPaginationQuery>
    {
        private readonly IReadDbContextSpecies _readDbContext;

        public GetBreedsWithPaginationHandler(IReadDbContextSpecies readDbContext)
        {
            _readDbContext = readDbContext;
        }

        public async Task<Result<PagedList<BreedDto>>> Execute(
            GetBreedsWithPaginationQuery query,
            CancellationToken token)
        {
            var speciesResult = await _readDbContext.Species
                .FirstOrDefaultAsync(s => s.Id == query.SpeciesId, token);

            if (speciesResult == null)
            {
                return Errors.General.NotFound(query.SpeciesId);
            }

            var breedQuery = _readDbContext.Breeds
                .Where(b => b.SpeciesId == query.SpeciesId);

            breedQuery = query.SortDirection?.ToLower() == Constants.SORT_DESCENDING
                ? breedQuery.OrderByDescending(s => s.Name)
                : breedQuery.OrderBy(s => s.Name);

            return await breedQuery
                    .ToPagedList(query.Page, query.PageSize, token);
        }
    }
}
