using PetHome.Shared.Core.Abstractions;
using PetHome.Shared.Core.Dtos;

namespace PetHome.Volunteers.Application.VolunteersManagement.Commands.UpdateRequisites
{
    public record UpdateRequisitesCommand(
        Guid VolunteerId,
        IEnumerable<RequisiteDto> Requisites) : ICommand;
}
