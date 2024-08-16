using PetHome.Domain.Shared;

namespace PetHome.Domain.Models.Pets
{
    public class Breed : Entity<BreedId>
    {
        private Breed(BreedId id) : base(id)
        {
        }

        public string Name { get; private set; }
    }
}
