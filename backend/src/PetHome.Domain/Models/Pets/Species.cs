using PetHome.Domain.Shared;

namespace PetHome.Domain.Models.Pets
{
    public class Species : Entity<SpeciesId>
    {        
        private Species(SpeciesId id) : base(id)
        {
        }

        public string Name { get; private set; }
        public List<Breed> Breeds { get; private set; }
    }
}
