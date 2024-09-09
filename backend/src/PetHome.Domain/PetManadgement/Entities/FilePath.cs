using PetHome.Domain.Shared;

namespace PetHome.Domain.PetManadgement.Entities
{
    public record FilePath
    {
        private FilePath(string path)
        {
            Path = path;           
        }

        public string Path { get; }       

        public static Result<FilePath> Create(Guid path, string extension)
        {
            var fullPath = path + "." + extension;
            return new FilePath(fullPath);
        }
    }
}
