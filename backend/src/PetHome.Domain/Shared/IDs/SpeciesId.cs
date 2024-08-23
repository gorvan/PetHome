namespace PetHome.Domain.Shared.IDs
{
    public record SpeciesId
    {
        private SpeciesId(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }

        public static SpeciesId NewSpeciesId() => new(Guid.NewGuid());
        public static SpeciesId Empty() => new(Guid.Empty);
        public static SpeciesId Create(Guid id) => new(id);
    }
}
