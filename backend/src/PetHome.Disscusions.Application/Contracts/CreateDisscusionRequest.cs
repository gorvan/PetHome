using PetHome.Disscusions.Application.DisscusionManagement.Commands.CreateDisscusion;

namespace PetHome.Disscusions.Application.Contracts;
public record CreateDisscusionRequest(Guid relationId, List<Guid> userIds)
{
    public CreateDisscusionCommand ToCommand() => 
        new CreateDisscusionCommand(relationId, userIds);
}

