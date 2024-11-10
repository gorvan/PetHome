using PetHome.Shared.Core.Abstractions;

namespace PetHome.Accounts.Application.AccountsMenagement.Queries.GetAccountById;
public record GetUserByIdQuery(Guid UserId) : IQuery;

