using PetHome.Domain.Shared;
using PetHome.Domain.Shared.IDs;

namespace PetHome.Domain.SpeciesManagement.AggregateRoot
{
    public record SpeciesBreedValue
    {
        private SpeciesBreedValue(SpeciesId speciesId, BreedId breedId)
        {
            SpeciesId = speciesId;
            BreedId = breedId;
        }

        public SpeciesId SpeciesId { get; } = default!;
        public BreedId BreedId { get; } = default!;

        public static Result<SpeciesBreedValue> Create(SpeciesId speciesId, BreedId breedId)
        {
            if (speciesId is null)
            {
                return $"{nameof(SpeciesId)} can not be null";
            }

            if (breedId is null)
            {
                return $"{nameof(BreedId)} can not be null";
            }

            var speciesBreedValue = new SpeciesBreedValue(speciesId, breedId);
            return speciesBreedValue;
        }
    }
}
