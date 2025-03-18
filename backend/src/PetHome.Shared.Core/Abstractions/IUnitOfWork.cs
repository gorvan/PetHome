using System.Data.Common;

namespace PetHome.Shared.Core.Abstractions
{
    public interface IUnitOfWork
    {
        Task<DbTransaction> BeginTransaction(CancellationToken cancellationToken = default);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
