using PetHome.Domain.Shared.IDs;

namespace PetHome.Domain.SpeciesManagement.AggregateRoot
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
