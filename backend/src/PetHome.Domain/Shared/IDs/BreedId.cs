namespace PetHome.Domain.Shared.IDs
{
    public record BreedId
    {
        private BreedId(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }

        public static BreedId NewBreedId() => new(Guid.NewGuid());
        public static BreedId Empty() => new(Guid.Empty);
        public static BreedId Create(Guid id) => new(id);
    }
}
