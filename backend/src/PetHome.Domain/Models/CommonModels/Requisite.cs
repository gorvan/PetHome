using PetHome.Domain.Shared;

namespace PetHome.Domain.Models.CommonModels
{
    public class Requisite : Entity<RequisiteId>
    {
        private Requisite(RequisiteId id) : base(id)
        {
        }

        public string Name { get; private set; }
        public string Description { get; private set; }
    }
}
