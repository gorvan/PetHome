using Microsoft.EntityFrameworkCore;
using PetHome.Shared.Core.Shared;
using PetHome.Shared.Core.Shared.IDs;
using PetHome.Species.Application.SpeciesManagement;
using PetHome.Species.Domain;
using PetHome.Species.Infrastructure.DbContexts;

namespace PetHome.Species.Infrastructure.Repositories
{
    public class SpeciesRepository : ISpeciesRepository
    {
        private readonly WriteDbContext _dbContext;
        public SpeciesRepository(WriteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<Guid>> Add(SpeciesType species, CancellationToken token)
        {
            await _dbContext.Species.AddAsync(species, token);
            await _dbContext.SaveChangesAsync(token);
            return Result<Guid>.Success(species.Id.Id);
        }

        public async Task<Result<Guid>> Update(SpeciesType species, CancellationToken token)
        {
            _dbContext.Species.Attach(species);
            await _dbContext.SaveChangesAsync(token);
            return Result<Guid>.Success(species.Id.Id);
        }

        public async Task<Result<Guid>> Delete(SpeciesType species, CancellationToken token)
        {
            _dbContext.Species.Remove(species);
            await _dbContext.SaveChangesAsync(token);
            return Result<Guid>.Success(species.Id.Id);
        }

        public async Task<Result<SpeciesType>> GetById(SpeciesId speciesId, CancellationToken token)
        {
            var species = await _dbContext
                .Species
                .FirstOrDefaultAsync(s => s.Id == speciesId, token);

            if (species is null)
            {
                return Errors.General.NotFound(speciesId.Id);
            }

            return species;
        }
    }
}
