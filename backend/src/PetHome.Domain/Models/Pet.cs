using PetHome.Domain.Models.SecondaryModels;

namespace PetHome.Domain.Models
{
    public class Pet
    {
        public Guid Id { get; private set; }
        public string Nickname { get; private set; }
        public string Species { get; private set; }
        public string Description { get; private set; }
        public string Breed { get; private set; }
        public string Color { get; private set; }
        public string Health { get; private set; }
        public Adress Adress { get; private set; }
        public double Weight { get; private set; }
        public double Height { get; private set; }
        public string Phone { get; private set; }
        public bool IsNeutered { get; private set; }
        public DateTime BirthDay { get; private set; }
        public bool IsVaccinated { get; private set; }
        public HelpStatus HelpStatus { get; private set; }
        public List<Requisite> Detailes { get; private set; }
        public DateTime CreateTime { get; private set; }
    }
}
