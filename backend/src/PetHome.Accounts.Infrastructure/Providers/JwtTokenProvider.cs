using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PetHome.Accounts.Application.Abstractions;
using PetHome.Accounts.Domain;
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

            Claim[] claims = [
                new Claim(CustomClaims.Sub, user.Id.ToString()),
                new Claim(CustomClaims.Email, user.Email ?? "")
            ];

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
