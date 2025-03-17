using Microsoft.EntityFrameworkCore.Storage;
using PetHome.Shared.Core.Abstractions;
using System.Data.Common;

namespace PetHome.VolunteerRequests.Infrastructure.DbContexts;
public class UnitOfWork : IUnitOfWork
{
    private readonly WriteDbContext Context;

    public UnitOfWork(WriteDbContext context)
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
