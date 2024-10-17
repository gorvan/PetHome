using PetHome.Shared.Core.Dtos;
using PetHome.Shared.Core.Shared.IDs;
using PetHome.Species.Application;
using PetHome.Species.Contracts;
using PetHome.Species.Domain;

namespace PetHome.Species.Presentation
{
    public class SpeciesContract : ISpeciesContract
    {
        private readonly IReadDbContextSpecies _readDbContext;
        public SpeciesContract(IReadDbContextSpecies readDbContext)
        {
            _readDbContext = readDbContext;
        }

        public SpeciesBreedValue CreateSpeciesBreedValue(SpeciesId speciesId, BreedId breedId)
        {
            return new SpeciesBreedValue(speciesId, breedId);
        }

        public IQueryable<SpeciesDto> GetSpeciesDtos()
        {
            return _readDbContext.Species;
        }
    }
}
