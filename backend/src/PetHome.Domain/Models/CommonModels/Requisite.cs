using PetHome.Domain.Shared;

namespace PetHome.Domain.Models.CommonModels
{
    public class Requisite : Entity<RequisiteId>
    {
        private Requisite(RequisiteId id) : base(id)
        {
        }

        private Requisite(RequisiteId id, NotNullableString name, Description description)
            : base(id)
        {
            Name = name;
            DescriptionValue = description;
        }

        public NotNullableString Name { get; private set; }
        public Description DescriptionValue { get; private set; }
        

        public static Result<Requisite> Create(RequisiteId requisiteId, NotNullableString name, Description description)
        {
            if (name is null)
            {
                return $"{nameof(Requisite)} " + $"{nameof(name)}" + " can not be null";
            }

            if (description is null)
            {
                return $"{nameof(Requisite)} " + $"{nameof(description)}" + " can not be null";
            }

            var requisite = new Requisite(requisiteId, name, description);

            return requisite;
        }
    }
}