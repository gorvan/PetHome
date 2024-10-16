using PetHome.Shared.Core.Shared;
using PetHome.Shared.Core.Shared.IDs;

namespace PetHome.Volunteers.Domain.Entities
{
    public class PetPhoto : Entity<PetPhotoId>
    {
        private PetPhoto(PetPhotoId id) : base(id)
        {
        }

        private PetPhoto(PetPhotoId id, FilePath path, bool isMain)
            : base(id)
        {
            Path = path;
            IsMain = isMain;
        }

        public FilePath Path { get; } = default!;
        public bool IsMain { get; private set; } = false;
        public bool IsUploaded { get; } = false;

        public static Result<PetPhoto> Create(
            PetPhotoId id,
            FilePath path,
            bool isMain)
        {
            return new PetPhoto(id, path, isMain);
        }

        public void SetMain() => IsMain = true;

        public void ResetMain() => IsMain = false;
    }
}
