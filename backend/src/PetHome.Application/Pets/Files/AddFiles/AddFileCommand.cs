namespace PetHome.Application.Pets.Files.AddFiles
{
    public record AddFileCommand(Stream FileStream, string FilePath);
}
