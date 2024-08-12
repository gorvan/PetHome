namespace PetHome.Domain.Models
{
    public class HelthInfo
    {
        public Guid PetId { get; }
        public string Condition { get; set; }
        public string Description { get; set; } = string.Empty;
        public string History { get; set; } = string.Empty;
    }
}
