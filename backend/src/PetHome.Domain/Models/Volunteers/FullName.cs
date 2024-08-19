﻿using PetHome.Domain.Models.Pets;
using PetHome.Domain.Shared;

namespace PetHome.Domain.Models.Volunteers
{
    public record FullName
    {
        private FullName() { }
        private FullName(string firstName, string secondName, string surname) 
        { 
            FirstName = firstName;
            SecondNname = secondName;
            Surname = surname;
        }

        public string FirstName { get; } = default!;
        public string SecondNname { get; } = default!;
        public string Surname { get; } = default!;

        public static Result<FullName> Create(string firstName, string secondName, string surname)
        {
            if (string.IsNullOrWhiteSpace(firstName))
            {
                return $"{nameof(FullName)} " + $"{nameof(firstName)}" + " can not be empty";
            }

            if (string.IsNullOrWhiteSpace(secondName))
            {
                return $"{nameof(FullName)} " + $"{nameof(secondName)}" + " can not be empty";
            }

            if (string.IsNullOrWhiteSpace(surname))
            {
                return $"{nameof(FullName)} " + $"{nameof(surname)}" + " can not be empty";
            }

            var fullName = new FullName(firstName, secondName, surname);

            return fullName;
        }
    }
}
