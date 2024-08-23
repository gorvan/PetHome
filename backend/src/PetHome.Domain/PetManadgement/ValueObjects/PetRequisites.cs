using PetHome.Domain.Shared;

namespace PetHome.Domain.PetManadgement.ValueObjects
{
    public record PetRequisites
    {
        private PetRequisites() { }
        private PetRequisites(IEnumerable<Requisite> requisites)
        {
            _requisites = requisites.ToList();
        }

        private List<Requisite> _requisites { get; } = [];
        public IReadOnlyList<Requisite> Requisites => _requisites;


        public static Result<PetRequisites> Create(IEnumerable<Requisite> requisites)
        {
            if (requisites is null)
            {
                return $"{nameof(PetRequisites)} "
                    + $"{nameof(requisites)}" + " can not be null";
            }

            var requisitesValue = new PetRequisites(requisites);

            return requisitesValue;
        }
    }
}
