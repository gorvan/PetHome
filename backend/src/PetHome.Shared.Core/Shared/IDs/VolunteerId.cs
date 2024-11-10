using CSharpFunctionalExtensions;

namespace PetHome.Shared.Core.Shared.IDs
{
    public class VolunteerId : ValueObject
    {
        private VolunteerId(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }

        public static VolunteerId NewVolunteerId() => new(Guid.NewGuid());
        public static VolunteerId Empty() => new(Guid.Empty);
        public static VolunteerId Create(Guid id) => new(id);

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Id;
        }

        public static implicit operator Guid(VolunteerId volunteerId)
        {
            if (volunteerId is null)
            {
                throw new ArgumentNullException();
            }

            return volunteerId.Id;
        }
    }
}