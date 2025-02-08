using Microsoft.EntityFrameworkCore;
using PetHome.Disscusions.Application.DisscusionManagement;
using PetHome.Disscusions.Domain;
using PetHome.Disscusions.Infrastructure.DbContexts;
using PetHome.Shared.Core.Shared;
using PetHome.Shared.Core.Shared.IDs;

namespace PetHome.Disscusions.Infrastructure.Repositories;
public class DisscusionRepositiory : IDisscusionRepositiory
{
    private readonly WriteDbContext _writeDbContext;
    public DisscusionRepositiory(WriteDbContext writeDbContext)
    {
        _writeDbContext = writeDbContext;
    }

    public async Task<Result<Guid>> Add(Disscusion disscusion, CancellationToken token)
    {
        await _writeDbContext.Disscusions.AddAsync(disscusion, token);
        await _writeDbContext.SaveChangesAsync(token);
        return Result<Guid>.Success(disscusion.DisscusionId);
    }

    public async Task<Result<Disscusion>> GetById(DisscusionId id, CancellationToken token)
    {
        var disscusion = await _writeDbContext.Disscusions
            .FirstOrDefaultAsync(d => d.DisscusionId == id);

        if (disscusion is null)
        {
            return Errors.General.NotFound(id);
        }

        return disscusion;
    }

    public async Task<Result<Guid>> Update(Disscusion disscusion, CancellationToken token)
    {
        _writeDbContext.Disscusions.Attach(disscusion);
        await _writeDbContext.SaveChangesAsync();
        return Result<Guid>.Success(disscusion.DisscusionId);
    }
}
