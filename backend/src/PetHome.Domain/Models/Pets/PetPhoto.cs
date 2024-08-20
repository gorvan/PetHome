using PetHome.Domain.Shared;
using System.Xml.Linq;

namespace PetHome.Domain.Models.Pets
{
    public class PetPhoto : Entity<PetPhotoId>
    {
        private PetPhoto(PetPhotoId id) : base(id)
        {
        }

        private PetPhoto(PetPhotoId id, string title, string path, bool isMain)
            :base(id)
        {
            Title = title;
            Path = path;
            IsMain = isMain;
        }

        public string Title { get; private set; }
        public string Path { get; private set; }
        public bool IsMain { get; private set; }

        public static Result<PetPhoto> Create(PetPhotoId id, string title, string path, bool isMain)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                return $"{nameof(PetPhoto)} " + $"{nameof(title)}" + " can not be empty";
            }

            if (string.IsNullOrWhiteSpace(path))
            {
                return $"{nameof(PetPhoto)} " + $"{nameof(path)}" + " can not be empty";
            }
           
            var photo = new PetPhoto(id, title, path, isMain);  

            return photo;
        }
    }
}
