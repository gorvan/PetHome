using PetHome.Domain.Shared;

namespace PetHome.Domain.Models.CommonModels
{
    public record RecuisiteCollection
    {
        private RecuisiteCollection() { }
        private RecuisiteCollection(List<Requisite> collectionValues)
        {
            _collectionValues = collectionValues;
        }

        private List<Requisite> _collectionValues { get; } = [];
        public IReadOnlyList<Requisite> CollectionValues => _collectionValues;


        public static Result<RecuisiteCollection> Create(List<Requisite> requisites)
        {
            if (requisites is null)
            {
                return $"{nameof(RecuisiteCollection)} " + $"{nameof(requisites)}" + " can not be null";
            }

            var requisiteColl = new RecuisiteCollection(requisites);

            return requisiteColl;
        }
    }
}
