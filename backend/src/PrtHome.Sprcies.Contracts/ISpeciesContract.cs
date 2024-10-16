using PetHome.Shared.Core.Dtos;
using PetHome.Shared.Core.Shared.IDs;
using PetHome.Species.Domain;

namespace PetHome.Species.Contracts
{
    public interface ISpeciesContract
    {
        IQueryable<SpeciesDto> GetSpeciesDtos();
        SpeciesBreedValue CreateSpeciesBreedValue(SpeciesId speciesId, BreedId breedId);

    }
}
