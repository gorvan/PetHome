using Microsoft.EntityFrameworkCore;
using PetHome.Application.SpeciesManagement;
using PetHome.Domain.Shared;
using PetHome.Domain.Shared.IDs;
using PetHome.Domain.SpeciesManagement.Entities;
using PetHome.Infrastructure.DbContexts;

namespace PetHome.Infrastructure.Repositories
{
    public class SpeciesRepository : ISpeciesRepository
    {
        private readonly WriteDbContext _dbContext;
        public SpeciesRepository(WriteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<Guid>> Add(Species species, CancellationToken token)
        {
            await _dbContext.Species.AddAsync(species, token);
            await _dbContext.SaveChangesAsync(token);
            return Result<Guid>.Success(species.Id.Id);
        }

        public async Task<Result<Guid>> Update(Species species, CancellationToken token)
        {
            _dbContext.Species.Attach(species);
            await _dbContext.SaveChangesAsync(token);
            return Result<Guid>.Success(species.Id.Id);
        }

        public async Task<Result<Guid>> Delete(Species species, CancellationToken token)
        {
            _dbContext.Species.Remove(species);
            await _dbContext.SaveChangesAsync(token);
            return Result<Guid>.Success(species.Id.Id);
        }

        public async Task<Result<Species>> GetById(SpeciesId speciesId, CancellationToken token)
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
