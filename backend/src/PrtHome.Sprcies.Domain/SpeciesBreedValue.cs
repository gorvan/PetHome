using PetHome.Shared.Core.Shared.IDs;

namespace PetHome.Species.Domain
{
    public record SpeciesBreedValue
    {
        public SpeciesBreedValue(SpeciesId speciesId, BreedId breedId)
        {
            SpeciesId = speciesId;
            BreedId = breedId;
        }

        public SpeciesId SpeciesId { get; } = default!;
        public BreedId BreedId { get; } = default!;
    }
}
