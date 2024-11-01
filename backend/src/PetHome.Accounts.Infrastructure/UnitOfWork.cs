using Microsoft.EntityFrameworkCore.Storage;
using PetHome.Shared.Core.Abstractions;
using System.Data.Common;

namespace PetHome.Accounts.Infrastructure
{
    public class UnitOfWork(AccountsDbContext accountsDbContext) : IUnitOfWork
    {
        public async Task<DbTransaction> BeginTransaction(
            CancellationToken cancellationToken = default)
        {
            var transaction = await accountsDbContext.Database
                .BeginTransactionAsync(cancellationToken);

            return transaction.GetDbTransaction();
        }

        public async Task SaveChanges(CancellationToken cancellationToken = default)
        {
            await accountsDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
