using PetHome.Domain.Shared;
using PetHome.Domain.Shared.IDs;

namespace PetHome.Domain.PetManadgement.Entities
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
        public bool IsMain { get; }

        public static Result<PetPhoto> Create(
            PetPhotoId id,
            FilePath path,
            bool isMain)
        {
            return new PetPhoto(id, path, isMain);
        }
    }    
}
