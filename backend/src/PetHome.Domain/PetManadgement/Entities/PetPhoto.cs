using PetHome.Domain.Shared;
using PetHome.Domain.Shared.IDs;

namespace PetHome.Domain.PetManadgement.Entities
{
    public class PetPhoto : Entity<PetPhotoId>
    {
        private PetPhoto(PetPhotoId id) : base(id)
        {
        }

        private PetPhoto(PetPhotoId id, string path, bool isMain)
            : base(id)
        {
            Path = path;
            IsMain = isMain;
        }

        public string Path { get; } = default!;
        public bool IsMain { get; }

        public static Result<PetPhoto> Create(
            PetPhotoId id,
            string path,
            bool isMain)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                return $"{nameof(PetPhoto)} "
                    + $"{nameof(path)}" + " can not be empty";
            }

            var photoValue = new PetPhoto(id, path, isMain);

            return photoValue;
        }
    }
}
