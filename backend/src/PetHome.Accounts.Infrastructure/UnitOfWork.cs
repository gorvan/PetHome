using Microsoft.EntityFrameworkCore.Storage;
using PetHome.Shared.Core.Abstractions;
using System.Data.Common;

namespace PetHome.Accounts.Infrastructure;
public class UnitOfWork : IUnitOfWork
{
    private readonly AccountsDbContext Context;

    public UnitOfWork(AccountsDbContext context)
    {
        Context = context;
    }

    public async Task<DbTransaction> BeginTransaction(CancellationToken cancellationToken)
    {
        var transaction = await Context.Database.BeginTransactionAsync(cancellationToken);

        return transaction.GetDbTransaction();
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        return await Context.SaveChangesAsync(cancellationToken);
    }
}
