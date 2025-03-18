using PetHome.Disscusions.Application.DisscusionManagement.Commands.CloseDisscusion;
using PetHome.Disscusions.Application.DisscusionManagement.Commands.CreateDisscusion;
using PetHome.Disscusions.Contracts;
using PetHome.Shared.Core.Shared;

namespace PetHome.Disscusions.Presentation;
public class DisscusionContract : IDisscusionContract
{
    private readonly CreateDisscusionHandler _createDisscusionHandler;
    private readonly CloseDisscusionHandler _closeDisscusionHandler;

    public DisscusionContract(
        CreateDisscusionHandler createDisscusionHandler,
        CloseDisscusionHandler closeDisscusionHandler)
    {
        _createDisscusionHandler = createDisscusionHandler;
        _closeDisscusionHandler = closeDisscusionHandler;
    }

    public async Task<Result<Guid>> CreateDisscusion(
        Guid relationId,
        List<Guid> users,
        CancellationToken cancellationToken)
    {
        var command = new CreateDisscusionCommand(relationId, users);
        return await _createDisscusionHandler.Execute(command, cancellationToken);
    }

    public async Task<Result<Guid>> CloseDisscusion(Guid disscussionId, CancellationToken cancellationToken)
    {
        var command = new CloseDisscusionCommand(disscussionId);
        return await _closeDisscusionHandler.Execute(command, cancellationToken);
    }

}
