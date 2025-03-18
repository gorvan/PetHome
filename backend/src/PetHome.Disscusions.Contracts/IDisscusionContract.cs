using PetHome.Shared.Core.Shared;

namespace PetHome.Disscusions.Contracts;
public interface IDisscusionContract
{
    Task<Result<Guid>> CreateDisscusion(
        Guid relationId,
        List<Guid> users,
        CancellationToken cancellationToken);

    Task<Result<Guid>> CloseDisscusion(Guid disscussionId, CancellationToken cancellationToken);
}
