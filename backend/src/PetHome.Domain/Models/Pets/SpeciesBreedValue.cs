namespace PetHome.Domain.Models.Pets
{
    public record SpeciesBreedValue
    {        
        private SpeciesBreedValue() { }

        private SpeciesBreedValue(SpeciesId speciesId, BreedId breedId)
        {
            SpeciesId = speciesId;
            BreedId = breedId;
        }

        public SpeciesId SpeciesId { get; } = default!;
        public BreedId BreedId { get; } = default!;
        
        public static SpeciesBreedValue Create(SpeciesId speciesId, BreedId breedId)
            => new(speciesId, breedId);
    }
}
