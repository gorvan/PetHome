using PetHome.Domain.Shared;

namespace PetHome.Domain.PetManadgement.ValueObjects
{
    public record VolunteersRequisites
    {
        public VolunteersRequisites() { }
        public VolunteersRequisites(IEnumerable<Requisite> requisites)
        {
            Requisites = requisites.ToList();
        }

        public IReadOnlyList<Requisite> Requisites { get; }
    }
}
