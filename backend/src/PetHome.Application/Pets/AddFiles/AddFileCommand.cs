namespace PetHome.Application.Pets.AddFiles
{
    public record AddFileCommand(Stream FileStraem, string FilePath, string BucketName);
   
}
