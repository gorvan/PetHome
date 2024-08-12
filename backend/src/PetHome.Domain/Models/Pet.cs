namespace PetHome.Domain.Models
{
    public class Pet
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Species { get; set; }
        public string Description { get; set; }
        public string Breed { get; set; }
        public string Color { get; set; }
        public string Helth { get; set; }
        public string Adress { get; set; }
        public double Wheight { get; set; }
        public double Height { get; set; }
        public string Phone { get; set; }
        public bool IsNeutered { get; set; }
        public DateTime BirthDay { get; set; }
        public bool IsVaccinated { get; set; }
        public int HelpState { get; set; }
        public List<Detailes> Detailes { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
