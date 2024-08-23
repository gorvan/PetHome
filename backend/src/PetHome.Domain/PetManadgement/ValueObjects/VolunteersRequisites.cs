using PetHome.Domain.Shared;

namespace PetHome.Domain.PetManadgement.ValueObjects
{
    public record VolunteersRequisites
    {
        private VolunteersRequisites() { }
        private VolunteersRequisites(IEnumerable<Requisite> requisites)
        {
            _requisites = requisites.ToList();
        }

        private List<Requisite> _requisites { get; } = [];
        public IReadOnlyList<Requisite> Requisites => _requisites;


        public static Result<VolunteersRequisites> Create(List<Requisite> requisites)
        {
            var requisitesValue = new VolunteersRequisites(requisites);

            return requisitesValue;
        }
    }
}
