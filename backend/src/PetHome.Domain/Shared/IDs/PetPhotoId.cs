namespace PetHome.Domain.Shared.IDs
{
    public record PetPhotoId
    {
        private PetPhotoId(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }

        public static PetPhotoId NewPhotoId() => new(Guid.NewGuid());
        public static PetPhotoId Empty() => new(Guid.Empty);
        public static PetPhotoId Create(Guid id) => new(id);
    }
}
