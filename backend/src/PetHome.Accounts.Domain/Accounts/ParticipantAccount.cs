﻿using PetHome.Shared.Core.Shared;

namespace PetHome.Accounts.Domain.Accounts
{
    public class ParticipantAccount
    {
        public const string PARTICIPANT = nameof(PARTICIPANT);

        private ParticipantAccount() { }

        public ParticipantAccount(FullName fullName, User user)
        {
            Id = Guid.NewGuid();
            User = user;
            FullName = fullName;
        }

        public Guid Id { get; set; }
        public User User { get; set; }
        public FullName FullName { get; set; }
    }
}
