﻿using Microsoft.AspNetCore.Identity;

namespace PetHome.Accounts.Domain
{
    public class Role : IdentityRole<Guid>
    {
        public List<RolePermission> RolePermission { get; set; } = [];   
    }
}
