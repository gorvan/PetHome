﻿namespace PetHome.Accounts.Domain
{
    public class Permission
    {
        public Guid Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
