using PetHome.Shared.Core.Abstractions;

namespace PetHome.Disscusions.Application.DisscusionManagement.Commands.CreateDisscusion;
public record CreateDisscusionCommand(Guid RelationId, List<Guid> UserIds) : ICommand;

