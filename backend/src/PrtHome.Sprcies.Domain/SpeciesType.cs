using PetHome.Shared.Core.Shared;
using PetHome.Shared.Core.Shared.IDs;
using PrtHome.Species.Domain.ValueObjects;

namespace PetHome.Species.Domain
{
    public class SpeciesType : Entity<SpeciesId>
    {
        private List<Breed> _breeds = [];
        private SpeciesType(SpeciesId id) : base(id)
        {
        }

        private SpeciesType(
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


        public static Result<SpeciesType> Create(
            SpeciesId id,
            string name,
            IEnumerable<Breed> breeds)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return Errors.General.ValueIsRequeired("Species.Name");
            }

            return new SpeciesType(id, name, breeds);
        }

        public void AddBreed(Breed breed)
        {
            _breeds.Add(breed);
        }
    }
}
