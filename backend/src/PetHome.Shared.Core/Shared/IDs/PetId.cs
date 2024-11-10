using CSharpFunctionalExtensions;

namespace PetHome.Shared.Core.Shared.IDs
{
    public class PetId : ValueObject
    {
        private PetId(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }

        public static PetId NewPetId() => new PetId(Guid.NewGuid());
        public static PetId Empty() => new PetId(Guid.Empty);
        public static PetId Create(Guid id) => new PetId(id);

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Id;
        }
    }
}
