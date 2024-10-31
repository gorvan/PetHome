﻿using PetHome.Shared.Core.Shared;

namespace PetHome.Accounts.Domain.Accounts
{
    public class VolunteerAccount
    {
        public const string VOLUNTEER = nameof(VOLUNTEER);

        private VolunteerAccount() { }

        public VolunteerAccount(FullName fullName, User user)
        {
            Id = Guid.NewGuid();
            UserId = user.Id;
            FullName = fullName;
        }

        private List<Requisite> _requisites = default!;

        private List<Description> _sertificates = default!;

        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public FullName FullName { get; set; }
        public int Expirience { get; set; }
        public IEnumerable<Requisite> Requisites => _requisites;
        public IEnumerable<Description> Sertificates => _sertificates;
    }
}
