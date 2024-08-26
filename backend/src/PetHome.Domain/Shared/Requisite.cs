namespace PetHome.Domain.Shared
{
    public record Requisite
    {
        private Requisite() { }
        private Requisite(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public string Name { get; } = default!;
        public string Description { get; } = default!;


        public static Result<Requisite> Create(string name, string description)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return Errors.General.ValueIsRequeired("Requisite.Name");
            }

            if (string.IsNullOrWhiteSpace(description)
                || description.Length > Constants.MAX_TEXT_LENGTH)
            {
                return Errors.General.ValueIsInvalid("Requisite.Description");
            }

            return new Requisite(name, description);
        }
    }
}