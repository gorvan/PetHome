using FluentValidation;

namespace PetHome.Accounts.Application.AccountsMenagement.Queries.GetAccountById;
public class GetUserByIdQueryValidation : AbstractValidator<GetUserByIdQuery>
{
    public GetUserByIdQueryValidation()
    {
        RuleFor(a => a.UserId).NotEmpty();
    }
}
