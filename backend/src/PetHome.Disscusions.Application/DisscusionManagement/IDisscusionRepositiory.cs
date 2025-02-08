using PetHome.Disscusions.Domain;
using PetHome.Shared.Core.Shared;
using PetHome.Shared.Core.Shared.IDs;

namespace PetHome.Disscusions.Application.DisscusionManagement;
public interface IDisscusionRepositiory
{
    Task<Result<Guid>> Add(Disscusion disscusion, CancellationToken token);
    Task<Result<Guid>> Update(Disscusion disscusion, CancellationToken token);
    Task<Result<Disscusion>> GetById(DisscusionId id, CancellationToken token);
}
