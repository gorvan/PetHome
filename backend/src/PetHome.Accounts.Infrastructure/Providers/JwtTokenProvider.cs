using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PetHome.Accounts.Domain;
using PetHome.Accounts.Infrastructure.Abstractions;
using PetHome.Accounts.Infrastructure.Authorization;
using PetHome.Accounts.Infrastructure.IdentityManager;
using PetHome.Accounts.Infrastructure.Models;
using PetHome.Accounts.Infrastructure.Options;
using PetHome.Shared.Core.Shared;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PetHome.Accounts.Infrastructure.Providers
{
    public class JwtTokenProvider : ITokenProvider
    {
        private readonly JwtOtions _jwtOtions;
        private readonly RefreshSessionOptions _sessionOptions;
        private readonly AccountsDbContext _accountsDbContext;
        private readonly PermissionsManager _permissionsManager;

        public JwtTokenProvider(
            IOptions<JwtOtions> options,
            IOptions<RefreshSessionOptions> sessionOptions,
            AccountsDbContext accountsDbContext,
            PermissionsManager permissionsManager)
        {
            _jwtOtions = options.Value;
            _sessionOptions = sessionOptions.Value;
            _accountsDbContext = accountsDbContext;
            _permissionsManager = permissionsManager;
        }

        public async Task<JwtTokenResult> GenerateAccessToken(User user, CancellationToken cancellationToken)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOtions.Key));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var roleClaims = user.Roles.Select(r => new Claim(CustomClaims.Role, r.Name ?? string.Empty));

            var permissions = await _permissionsManager.GetUserPermissions(user.Id, cancellationToken);
            var permissionClaims = permissions.Select(p => new Claim(CustomClaims.Permission, p));

            var jti = Guid.NewGuid();

            Claim[] claims = [
                new Claim(CustomClaims.Id, user.Id.ToString()),
                new Claim(CustomClaims.Jti, jti.ToString()),
                new Claim(CustomClaims.Email, user.Email ?? ""),
            ];

            claims = claims.Concat(roleClaims).Concat(permissionClaims).ToArray();

            var jwtToken = new JwtSecurityToken(
                issuer: _jwtOtions.Issuer,
                audience: _jwtOtions.Audience,
                expires: DateTime.UtcNow.AddMinutes(int.Parse(_jwtOtions.ExpiredMinutesTime)),
                signingCredentials: signingCredentials,
                claims: claims);

            var jwtStringToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);

            return new JwtTokenResult(jwtStringToken, jti);
        }

        public async Task<Guid> GenerateRefreshToken(User user, Guid accessTokenJti, CancellationToken cancellationToken)
        {
            var refreshSession = new RefreshSession
            {
                User = user,
                UserId = user.Id,
                CreatedAt = DateTime.UtcNow,
                ExpiresIn = DateTime.UtcNow.AddDays(_sessionOptions.ExpiredDaysTime),
                Jti = accessTokenJti,
                RefreshToken = Guid.NewGuid()
            };

            _accountsDbContext.Add(refreshSession);
            await _accountsDbContext.SaveChangesAsync(cancellationToken);

            return refreshSession.RefreshToken;
        }

        public async Task<Result<IReadOnlyList<Claim>>> GetUserClaims(string jwtToken, CancellationToken cancellationToken)
        {
            var jwtHandler = new JwtSecurityTokenHandler();

            var validationParameters = TokenValidationParametersFactory.CreateWithoutLifeTime(_jwtOtions);

            var validationResult = await jwtHandler.ValidateTokenAsync(jwtToken, validationParameters);

            if (validationResult.IsValid == false)
            {
                return Errors.Tokens.InvalidToken();
            }

            return validationResult.ClaimsIdentity.Claims.ToList();
        }
    }
}
