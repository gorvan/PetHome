using Microsoft.IdentityModel.Tokens;
using PetHome.Accounts.Infrastructure.Providers;
using System.Text;

namespace PetHome.Accounts.Infrastructure.Authorization
{
    public static class TokenValidationParametersFactory
    {
        public static TokenValidationParameters CreateWithLifeTime(JwtOtions jwtOtions)
        {
            return new()
            {
                ValidIssuer = jwtOtions.Issuer,
                ValidAudience = jwtOtions.Audience,
                IssuerSigningKey =
                   new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOtions.Key)),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,                
                ClockSkew = TimeSpan.FromSeconds(1)
            };
        }

        public static TokenValidationParameters CreateWithoutLifeTime(JwtOtions jwtOtions)
        {
            return new()
            {
                ValidIssuer = jwtOtions.Issuer,
                ValidAudience = jwtOtions.Audience,
                IssuerSigningKey =
                   new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOtions.Key)),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true,
                ClockSkew = TimeSpan.FromSeconds(1)
            };
        }
    }
}
