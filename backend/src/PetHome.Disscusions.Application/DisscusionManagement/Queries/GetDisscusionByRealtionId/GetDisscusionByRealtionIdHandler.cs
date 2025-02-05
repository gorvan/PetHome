using Microsoft.EntityFrameworkCore;
using PetHome.Shared.Core.Abstractions;
using PetHome.Shared.Core.Dtos;
using PetHome.Shared.Core.Shared;

namespace PetHome.Disscusions.Application.DisscusionManagement.Queries.GetDisscusionByRealtionId;
public class GetDisscusionByRelationIdHandler :
    IQueryHandler<Result<DisscusionDto>, GetDisscusionByRelationIdQuery>
{
    private readonly IReadDbContextDisscusions _readDbContext;

    public GetDisscusionByRelationIdHandler(IReadDbContextDisscusions readDbContext)
    {
        _readDbContext = readDbContext;
    }

    public async Task<Result<DisscusionDto>> Execute(
        GetDisscusionByRelationIdQuery query,
        CancellationToken token)
    {
        var disscusion = await _readDbContext.Disscusions
            .FirstOrDefaultAsync(d => d.RelationId == query.RelationId);

        if (disscusion is null)
        {
            return Errors.General.NotFound(query.RelationId);
        }

        return disscusion;
    }
}
