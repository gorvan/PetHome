using PetHome.Accounts.Domain;
using PetHome.Accounts.Domain.Accounts;
using PetHome.Shared.Core.Dtos;

namespace PetHome.Accounts.Application.AccountsMenagement.Queries.GetAccountById;

public class UserMapper
{
    public UserDto MapFromUser(User user, ParticipantAccount participantAccount)
    {
        var roles = user.Roles.Select(r => new RoleDto
        {
            Id = r.Id,
            Name = r.Name ?? string.Empty,
            Permissions = r.RolePermission.Select(rp => new PermissionDto
            {
                Id = rp.Permission.Id,
                Code = rp.Permission.Code,
                Description = rp.Permission.Description
            }).ToArray()
        });

        var participantAccountDto = new AccountDto
        {
            Id = participantAccount.Id,
            FirstName = participantAccount.FullName.FirstName,
            SecondName = participantAccount.FullName.SecondName,
            Surname =participantAccount.FullName.Surname
        };


        var userDto = new UserDto
        {
            Id = user.Id,
            Name = user.UserName ?? string.Empty,
            Roles = roles.ToArray(),
            SocialNetworks = user.SocialNetworks
                .Select(s => new SocialNetworkDto(s.Name, s.Link))
                .ToArray(),
            Participant = participantAccountDto
        };

        return userDto;
    }
}
