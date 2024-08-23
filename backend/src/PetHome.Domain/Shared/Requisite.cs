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
            if (name is null)
            {
                return $"{nameof(Requisite)} " + $"{nameof(name)}" + " can not be null";
            }

            if (description is null)
            {
                return $"{nameof(Requisite)} " + $"{nameof(description)}" + " can not be null";
            }

            var requisite = new Requisite(name, description);

            return requisite;
        }
    }
}