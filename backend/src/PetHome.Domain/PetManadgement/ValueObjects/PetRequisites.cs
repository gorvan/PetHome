using PetHome.Domain.Shared;

namespace PetHome.Domain.PetManadgement.ValueObjects
{
    public record PetRequisites
    {
        public PetRequisites() { }
        public PetRequisites(IEnumerable<Requisite> requisites)
        {
            Requisites = requisites.ToList();
        }

        public IReadOnlyList<Requisite> Requisites { get; } = [];
    }
}
