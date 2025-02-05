using PetHome.Shared.Core.Abstractions;

namespace PetHome.Disscusions.Application.DisscusionManagement.Commands.CloseDisscusion;
public record CloseDisscusionCommand(Guid DisscusionId) : ICommand;
