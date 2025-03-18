using Microsoft.EntityFrameworkCore.Storage;
using PetHome.Shared.Core.Abstractions;
using System.Data.Common;

namespace PetHome.Accounts.Infrastructure;
public class UnitOfWork : IUnitOfWork
{
    private readonly AccountsDbContext _context;

    public UnitOfWork(AccountsDbContext context)
    {
        _context = context;
    }

    public async Task<DbTransaction> BeginTransaction(CancellationToken cancellationToken)
    {
        var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);

        return transaction.GetDbTransaction();
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }
}
