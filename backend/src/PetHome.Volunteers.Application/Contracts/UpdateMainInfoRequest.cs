using PetHome.Shared.Core.Dtos;
using PetHome.Volunteers.Application.VolunteersManagement.Commands.UpdateMainInfo;

namespace PetHome.Volunteers.Application.Contracts
{
    public record UpdateMainInfoRequest(
    FullNameDto FullName,
    string Email,
    string Phone,
    string Description)
    {
        public UpdateMainInfoCommand ToCommand(Guid id) =>
              new UpdateMainInfoCommand(
                id,
                FullName,
                Email,
                Phone,
                Description);
    }
}
