namespace PetHome.Shared.Core.Shared.IDs
{
    public record PetId : IComparable<PetId>
    {
        private PetId(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }

        public static PetId NewPetId() => new PetId(Guid.NewGuid());
        public static PetId Empty() => new PetId(Guid.Empty);
        public static PetId Create(Guid id) => new PetId(id);

        public int CompareTo(PetId? other)
        {
            return this.Id.CompareTo(other?.Id);
        }
    }
}
