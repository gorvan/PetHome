using Microsoft.EntityFrameworkCore;
using PetHome.Accounts.Infrastructure;
using PetHome.Accounts.Infrastructure.Mappers;
using PetHome.Shared.Core.Abstractions;
using PetHome.Shared.Core.Dtos;
using PetHome.Shared.Core.Shared;

namespace PetHome.Accounts.Application.AccountsMenagement.Queries.GetAccountById;
public class GetUserByIdHandler(
    AccountsDbContext accountsContext,
    UserMapper userMapper) : IQueryHandler<Result<UserDto>, GetUserByIdQuery>
{
    public async Task<Result<UserDto>> Execute(GetUserByIdQuery query, CancellationToken cancellationToken)
    {
        var userResult = await accountsContext.Users
            .Include(u => u.Roles)
            .Include(u => u.ParticipantAccount)
            .Include(u => u.VolunteerAccount)
            .FirstOrDefaultAsync(u => u.Id == query.UserId, cancellationToken);
        if (userResult == null)
        {
            return Errors.General.NotFound(query.UserId);
        }

        return userMapper.MapFromUser(userResult);
    }
}
