namespace PetHome.Domain.Models
{
    public class PetPhoto
    {
        public Guid Id { get; }
        public string? Title { get; }
        public string Path { get; }        
        public bool IsMain { get; }
    }
}
