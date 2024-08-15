using PetHome.Domain.Shared;

namespace PetHome.Domain.Models.Pets
{
    public class PetPhoto : Entity<PetPhotoId>
    {
        private PetPhoto(PetPhotoId id) : base(id)
        {
        }

        public string? Title { get; }
        public string Path { get; }
        public bool IsMain { get; }


    }
}
