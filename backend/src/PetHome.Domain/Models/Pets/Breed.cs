using PetHome.Domain.Models.CommonModels;
using PetHome.Domain.Shared;

namespace PetHome.Domain.Models.Pets
{
    public class Breed : Entity<BreedId>
    {
        private Breed(BreedId id) : base(id)
        {
        }

        private Breed(BreedId breedId, NotNullableString name)
            : base(breedId)
        {
            Name = name;
        }

        public NotNullableString Name { get; private set; }

        public static Result<Breed> Create(BreedId id, NotNullableString name)
        {
            if (name is null)
            {
                return $"{nameof(Breed)} " + $"{nameof(name)}" + " can not be null";
            }

            var breed = new Breed(id, name);
            return breed;
        }
    }
}
