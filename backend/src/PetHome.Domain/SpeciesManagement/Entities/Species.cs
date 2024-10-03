using PetHome.Domain.Shared;
using PetHome.Domain.Shared.IDs;
using PetHome.Domain.SpeciesManagement.ValueObjects;

namespace PetHome.Domain.SpeciesManagement.Entities
{
    public class Species : Entity<SpeciesId>
    {
        private List<Breed> _breeds = [];
        private Species(SpeciesId id) : base(id)
        {
        }

        private Species(
            SpeciesId id,
            string name,
            IEnumerable<Breed> breeds)
            : base(id)
        {
            Name = name;
            _breeds = breeds.ToList();
        }

        public string Name { get; } = default!;
        public IReadOnlyList<Breed>? Breeds => _breeds;


        public static Result<Species> Create(
            SpeciesId id,
            string name,
            IEnumerable<Breed> breeds)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return Errors.General.ValueIsRequeired("Species.Name");
            }

            return new Species(id, name, breeds);
        }

        public void AddBreed(Breed breed)
        {
            _breeds.Add(breed);
        }
    }
}
