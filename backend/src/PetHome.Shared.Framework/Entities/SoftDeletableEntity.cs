﻿using PetHome.Shared.Core.Shared;

namespace PetHome.Shared.Framework.Entities
{
    public abstract class SoftDeletableEntity<TId> : Entity<TId> where TId : IComparable<TId>
    {
        protected SoftDeletableEntity(TId id) : base(id)
        {
        }

        public virtual void Delete()
        {
            if (IsDeleted)
                return;

            IsDeleted = true;
            DeletionDate = DateTime.UtcNow;
        }

        public virtual void Restore()
        {
            if (!IsDeleted)
                return;

            IsDeleted = false;
            DeletionDate = null;
        }

        public bool IsDeleted { get; private set; }

        public DateTime? DeletionDate { get; private set; }

        public bool IsExpired =>
            DeletionDate != null
                && DateTime.UtcNow >= DeletionDate.Value
                    .AddDays(Constants.DELETE_EXPIRED_ENTITIES_DAYS);
    }
}
