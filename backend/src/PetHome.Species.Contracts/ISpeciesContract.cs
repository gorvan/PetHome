using PetHome.Shared.Core.Dtos;
using PetHome.Shared.Core.Shared;
using PetHome.Shared.Core.Shared.IDs;

namespace PetHome.Species.Contracts
{
    public interface ISpeciesContract
    {
        IQueryable<SpeciesDto> GetSpeciesDtos();
        SpeciesBreedValue CreateSpeciesBreedValue(SpeciesId speciesId, BreedId breedId);

    }
}
