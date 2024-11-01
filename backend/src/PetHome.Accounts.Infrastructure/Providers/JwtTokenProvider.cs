using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PetHome.Accounts.Domain;
using PetHome.Accounts.Infrastructure.Abstractions;
using PetHome.Accounts.Infrastructure.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PetHome.Accounts.Infrastructure.Providers
{
    public class JwtTokenProvider : ITokenProvider
    {
        private readonly JwtOtions _jwtOtions;

        public JwtTokenProvider(IOptions<JwtOtions> options)
        {
            _jwtOtions = options.Value;
        }

        public string GenerateAccessToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOtions.Key));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var roleClaims = user.Roles.Select(r => new Claim(CustomClaims.Role, r.Name ?? string.Empty));

            Claim[] claims = [
                new Claim(CustomClaims.Id, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email ?? ""),                
            ];

            claims = claims.Concat(roleClaims).ToArray();

            var jwtToken = new JwtSecurityToken(
                issuer: _jwtOtions.Issuer,
                audience: _jwtOtions.Audience,
                expires: DateTime.UtcNow.AddMinutes(int.Parse(_jwtOtions.ExpiredMinutesTime)),
                signingCredentials: signingCredentials,
                claims: claims);

            var stringToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);

            return stringToken;
        }
    }
}
