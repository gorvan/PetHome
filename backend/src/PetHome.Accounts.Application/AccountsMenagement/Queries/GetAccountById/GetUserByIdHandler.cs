using Microsoft.EntityFrameworkCore;
using PetHome.Accounts.Infrastructure;
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
            .FirstOrDefaultAsync(u => u.Id == query.UserId, cancellationToken);
        if (userResult == null)
        {
            return Errors.General.NotFound(query.UserId);
        }

        var participantAccount = await accountsContext.ParticipantAccounts
            .FirstOrDefaultAsync(p => p.Id == userResult.ParticipantAccountId, cancellationToken);
        if (participantAccount == null)
        {
            return Errors.General.NotFound(userResult.ParticipantAccountId);
        }


        return userMapper.MapFromUser(userResult, participantAccount);
    }
}
