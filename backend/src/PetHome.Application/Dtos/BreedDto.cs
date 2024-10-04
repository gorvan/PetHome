namespace PetHome.Application.Dtos
{
    public class BreedDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Guid SpeciesId { get; set; }
    }
}
