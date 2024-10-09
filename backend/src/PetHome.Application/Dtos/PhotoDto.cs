namespace PetHome.Application.Dtos
{
    public class PhotoDto
    {
        public Guid Id { get; set; } = default!;
        public string Path { get; set; } = string.Empty;
        public Guid PetId { get; set; } = default!;
        public bool IsMain { get; set; } = false;
    }
}
