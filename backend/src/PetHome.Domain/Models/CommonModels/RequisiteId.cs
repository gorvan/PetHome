using PetHome.Domain.Shared;

namespace PetHome.Domain.Models.CommonModels
{
    public record RequisiteId
    {
        private RequisiteId(Guid value)
        {
            Value = value;
        }

        public Guid Value { get; }

        public static RequisiteId NewRequisiteId() => new RequisiteId(Guid.NewGuid());
        public static RequisiteId Empty() => new RequisiteId(Guid.Empty);
        public static RequisiteId Create(Guid id) => new RequisiteId(id);
    }
}
