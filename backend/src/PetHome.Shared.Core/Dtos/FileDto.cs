namespace PetHome.Shared.Core.Dtos
{
    public record FileDto(
        Stream Stream,
        string FileName,
        string ContentType);
}
