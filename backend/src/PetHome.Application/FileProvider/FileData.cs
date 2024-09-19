namespace PetHome.Application.FileProvider
{
    public record FileData(
        Stream Stream,
        FileInfo Info);

    public record FileInfo(
        string BucketName,
        string FilePath);
}
