using PetHome.Shared.Core.Abstractions;

namespace PetHome.Accounts.Application.AccountsMenagement.Commands.RefreshTokens
{
    public record RefreshTokensCommand(string AccessToken, Guid RefreshToken) : ICommand;   
}
