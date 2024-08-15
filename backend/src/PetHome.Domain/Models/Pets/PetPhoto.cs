using PetHome.Domain.Shared;

namespace PetHome.Domain.Models.Pets
{
    public class PetPhoto : Entity<PetPhotoId>
    {
        private PetPhoto(PetPhotoId id) : base(id)
        {
        }

        public string? Title { get; private set; }
        public string Path { get; private set; }
        public bool IsMain { get; private set; }


    }
}
