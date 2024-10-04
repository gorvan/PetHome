using Microsoft.EntityFrameworkCore;
using PetHome.Application.Abstractions;
using PetHome.Application.Database;
using PetHome.Application.Dtos;
using PetHome.Application.Extensions;
using PetHome.Application.Models;
using PetHome.Domain.Shared;

namespace PetHome.Application.SpeciesManagement.Queries.GetBreeds
{
    public class GetBreedsWithPaginationHandler :
        IQueryHandler<Result<PagedList<BreedDto>>, GetBreedsWithPaginationQuery>
    {
        private readonly IReadDbContext _readDbContext;

        public GetBreedsWithPaginationHandler(IReadDbContext readDbContext)
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
