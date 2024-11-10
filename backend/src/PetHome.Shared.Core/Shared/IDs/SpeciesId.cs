using CSharpFunctionalExtensions;

namespace PetHome.Shared.Core.Shared.IDs
{
    public class SpeciesId : ValueObject
    {
        private SpeciesId(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }

        public static SpeciesId NewSpeciesId() => new(Guid.NewGuid());
        public static SpeciesId Empty() => new(Guid.Empty);
        public static SpeciesId Create(Guid id) => new(id);

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Id;
        }
    }
}
