using PetHome.Accounts.Domain;

namespace PetHome.Accounts.Infrastructure.Abstractions
{
    public interface ITokenProvider
    {
        string GenerateAccessToken(User user);
    }
}
