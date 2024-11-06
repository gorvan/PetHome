using PetHome.Accounts.Application.AccountsMenagement.Commands.RefreshTokens;

namespace PetHome.Accounts.Application.Contracts
{
    public record RefreshTokensRequest(string AccessToken, Guid RefreshToken)
    {
        public RefreshTokensCommand ToCommand() =>
            new RefreshTokensCommand(AccessToken, RefreshToken);
    } 
}
