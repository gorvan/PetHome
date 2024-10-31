using System.Data.Common;

namespace PetHome.Shared.Core.Abstractions
{
    public interface IUnitOfWork
    {
        Task<DbTransaction> BeginTravsaction(CancellationToken cancellationToken = default);
        Task SaveChanges(CancellationToken cancellationToken = default);
    }
}
