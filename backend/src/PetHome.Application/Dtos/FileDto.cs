namespace PetHome.Application.Dtos
{
    public record FileDto(
        Stream Stream,
        string FileName,
        string ContentType);
}
