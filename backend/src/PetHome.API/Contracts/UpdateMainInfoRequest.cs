using PetHome.Application.Dtos;
using PetHome.Application.VolunteersManagement.Commands.UpdateMainInfo;

namespace PetHome.Application.Volunteers.UpdateMainInfo
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
