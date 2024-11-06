namespace PetHome.Accounts.Infrastructure.Models
{
    public record JwtTokenResult(string AccessToken, Guid Jti);   
}