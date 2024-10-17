namespace PetHome.Shared.Core.Shared
{
    public interface ISoftDeletable
    {
        void Delete();
        void Restore();
    }
}
