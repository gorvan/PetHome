using CSharpFunctionalExtensions;

namespace PetHome.Shared.Core.Shared.IDs
{
    public class BreedId : ValueObject
    {
        private BreedId(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }

        public static BreedId NewBreedId() => new(Guid.NewGuid());
        public static BreedId Empty() => new(Guid.Empty);
        public static BreedId Create(Guid id) => new(id);

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Id;
        }
    }
}
