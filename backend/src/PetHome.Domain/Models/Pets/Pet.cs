using PetHome.Domain.Models.CommonModels;
using PetHome.Domain.Shared;

namespace PetHome.Domain.Models.Pets
{
    public class Pet : Entity<PetId>
    {
        private Pet(PetId id) : base(id)
        {
        }

        public string Nickname { get; private set; }
        public SpeciesBreedValue SpeciesBreedValue { get; private set; }
        public string Description { get; private set; }       
        public string Color { get; private set; }
        public string Health { get; private set; }
        public Address Address { get; private set; }
        public double Weight { get; private set; }
        public double Height { get; private set; }
        public string Phone { get; private set; }
        public bool IsNeutered { get; private set; }
        public DateTime BirthDay { get; private set; }
        public bool IsVaccinated { get; private set; }
        public HelpStatus HelpStatus { get; private set; }
        public List<Requisite> Detailes { get; private set; }
        public DateTime CreateTime { get; private set; }
        public List<PetPhoto> Photos { get; private set; }
    }
}
