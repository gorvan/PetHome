using Microsoft.EntityFrameworkCore.Storage;
using PetHome.Shared.Core.Abstractions;
using System.Data.Common;

namespace PetHome.VolunteerRequests.Infrastructure.DbContexts;
public class UnitOfWork : IUnitOfWork
{
    private readonly WriteDbContext _context;

    public UnitOfWork(WriteDbContext context)
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
