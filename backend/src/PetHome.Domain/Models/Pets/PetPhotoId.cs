namespace PetHome.Domain.Models.Pets
{
    public record PetPhotoId
    {
        private PetPhotoId(Guid value)
        {
            Value = value;
        }

        public Guid Value { get; }

        public static PetPhotoId NewPhotoId() => new(Guid.NewGuid());
        public static PetPhotoId Empty() => new(Guid.Empty);
        public static PetPhotoId Create(Guid id) => new(id);
    }
}
