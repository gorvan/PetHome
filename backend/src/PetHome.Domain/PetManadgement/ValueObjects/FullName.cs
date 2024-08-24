using PetHome.Domain.Shared;

namespace PetHome.Domain.PetManadgement.ValueObjects
{
    public record FullName
    {
        private FullName(string firstName, string secondName, string surname)
        {
            FirstName = firstName;
            SecondName = secondName;
            Surname = surname;
        }

        public string FirstName { get; } = default!;
        public string SecondName { get; } = default!;
        public string Surname { get; } = default!;

        public static Result<FullName> Create(
            string firstName,
            string secondName,
            string surname)
        {
            if (string.IsNullOrWhiteSpace(firstName))
            {
                return Errors.General.ValueIsRequeired("Name.FirstName");
            }

            if (string.IsNullOrWhiteSpace(secondName))
            {
                return Errors.General.ValueIsRequeired("Name.SecondName");
            }

            if (string.IsNullOrWhiteSpace(surname))
            {
                return Errors.General.ValueIsRequeired("Name.Surame");
            }

            return new FullName(firstName, secondName, surname);
        }
    }
}
