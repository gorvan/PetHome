using PetHome.Application.Abstractions;
using PetHome.Application.Dtos;

namespace PetHome.Application.VolunteersManagement.Commands.UpdateRequisites
{
    public record UpdateRequisitesCommand(
        Guid VolunteerId,
        IEnumerable<RequisiteDto> Requisites) : ICommand;
}
