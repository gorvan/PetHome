using PetHome.Shared.Core.Shared;

namespace PetHome.Volunteers.Domain.Entities
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
