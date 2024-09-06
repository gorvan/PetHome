namespace PetHome.Application.Pets.GetFiles
{
    public record GetFilesCommand(string BucketName, string FilePrefix);
}
