using PetHome.Domain.Models.Volunteers;
using PetHome.Domain.Shared;

namespace PetHome.Domain.Models.CommonModels
{
    public record Requisite 
    {
        private Requisite() { }
        private Requisite( string name, string description)            
        {
            Name = name;
            DescriptionValue = description;
        }

        public string Name { get; } = default!;
        public string DescriptionValue { get; } = default!;


        public static Result<Requisite> Create(string name, string description)
        {
            if (name is null)
            {
                return $"{nameof(Requisite)} " + $"{nameof(name)}" + " can not be null";
            }

            if (description is null)
            {
                return $"{nameof(Requisite)} " + $"{nameof(description)}" + " can not be null";
            }

            var requisite = new Requisite(name, description);

            return requisite;
        }
    }
}