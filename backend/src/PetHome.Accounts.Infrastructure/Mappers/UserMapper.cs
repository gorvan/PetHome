using PetHome.Accounts.Domain;
using PetHome.Shared.Core.Dtos;

namespace PetHome.Accounts.Infrastructure.Mappers;

public class UserMapper
{
    public UserDto MapFromUser(User user)
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
            Id = user.ParticipantAccount.Id,
            FirstName = user.ParticipantAccount.FullName.FirstName,
            SecondName = user.ParticipantAccount.FullName.SecondName,
            Surname = user.ParticipantAccount.FullName.Surname
        };

        AccountDto volunteerAccountDto = default!;

        if (user.VolunteerAccount != null)
        {
            volunteerAccountDto = new AccountDto
            {
                Id = user.VolunteerAccount.Id,
                FirstName = user.VolunteerAccount.FullName.FirstName,
                SecondName = user.VolunteerAccount.FullName.SecondName,
                Surname = user.VolunteerAccount.FullName.Surname
            };
        }

        var userDto = new UserDto
        {
            Id = user.Id,
            Name = user.UserName ?? string.Empty,
            Roles = roles.ToArray(),
            SocialNetworks = user.SocialNetworks
                .Select(s => new SocialNetworkDto(s.Name, s.Link))
                .ToArray(),
            Participant = participantAccountDto,
            Volunteer = volunteerAccountDto
        };

        return userDto;
    }
}
