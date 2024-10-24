using PetHome.Shared.Core.Shared;
using PetHome.Shared.Core.Shared.IDs;

namespace PrtHome.Species.Domain.ValueObjects
{
    public class Breed : Entity<BreedId>
    {
        private Breed(BreedId id) : base(id)
        {
        }

        private Breed(BreedId id, string name)
            : base(id)
        {
            Name = name;
        }

        public string Name { get; } = default!;

        public static Result<Breed> Create(BreedId id, string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return Errors.General.ValueIsRequeired("Breed.Name");
            }

            return new Breed(id, name);
        }
    }
}
