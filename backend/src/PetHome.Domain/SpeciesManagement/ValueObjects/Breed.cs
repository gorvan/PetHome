using PetHome.Domain.Shared;
using PetHome.Domain.Shared.IDs;

namespace PetHome.Domain.SpeciesManagement.ValueObjects
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
