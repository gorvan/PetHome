using PetHome.Accounts.Domain;

namespace PetHome.Accounts.Application.Abstractions
{
    public interface ITokenProvider
    {
        string GenerateAccessToken(User user);
    }
}
