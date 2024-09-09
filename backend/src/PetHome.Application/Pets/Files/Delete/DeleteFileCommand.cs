namespace PetHome.Application.Pets.Files.Delete
{
    public record DeleteFileCommand(string FilePath, string BucketName);
}
