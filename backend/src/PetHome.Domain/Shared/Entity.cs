namespace PetHome.Domain.Shared
{
    public abstract class Entity<TId> where TId : notnull
    {
        protected Entity(TId id)
        {
            Id = id;
        }

        protected TId Id { get; private set; }
    }
}
