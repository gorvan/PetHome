namespace PetHome.Application.Pets.Files.GetFiles
{
    public record GetFilesCommand(string BucketName, string FilePrefix);
}
