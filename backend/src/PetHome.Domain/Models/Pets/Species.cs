using PetHome.Domain.Models.CommonModels;
using PetHome.Domain.Shared;

namespace PetHome.Domain.Models.Pets
{
    public class Species : Entity<SpeciesId>
    {
        private List<Breed> _breeds = [];
        private Species(SpeciesId id) : base(id)
        {
        }

        private Species(SpeciesId id, NotNullableString name) : base(id)
        {
            Name = name;
        }

        public NotNullableString Name { get; private set; }
        public List<Breed> Breeds => _breeds;


        public static Result<Species> Create(SpeciesId id, NotNullableString name)
        {            
            var species = new Species(id, name);
            return species;
        }

        public Result AddBreed(Breed breed)
        {
            _breeds.Add(breed);

            return Result.Success();
        }
    }
}
