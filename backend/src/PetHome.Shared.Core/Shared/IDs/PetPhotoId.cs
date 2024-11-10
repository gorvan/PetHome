using CSharpFunctionalExtensions;

namespace PetHome.Shared.Core.Shared.IDs
{
    public class PetPhotoId : ValueObject
    {
        private PetPhotoId(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }

        public static PetPhotoId NewPhotoId() => new(Guid.NewGuid());
        public static PetPhotoId Empty() => new(Guid.Empty);
        public static PetPhotoId Create(Guid id) => new(id);

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Id;
        }
    }
}
