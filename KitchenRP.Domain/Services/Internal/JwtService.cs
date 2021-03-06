using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using KitchenRP.DataAccess.Repositories;
using Microsoft.IdentityModel.Tokens;
using NodaTime;
using NodaTime.Extensions;

namespace KitchenRP.Domain.Services.Internal
{
    public class JwtService : IJwtService
    {
        private readonly byte[] _accessSecret;
        private readonly int _accessTimeout;
        private readonly byte[] _refreshSecret;
        private readonly int _refreshTimeout;
        private readonly IRefreshTokenRepository _refreshTokenRepository;

        public JwtService(
            IRefreshTokenRepository refreshTokenRepository,
            byte[] accessSecret,
            int accessTimeout,
            byte[] refreshSecret,
            int refreshTimeout)
        {
            _refreshTokenRepository = refreshTokenRepository;
            _accessSecret = accessSecret;
            _accessTimeout = accessTimeout;
            _refreshSecret = refreshSecret;
            _refreshTimeout = refreshTimeout;
            Clock = SystemClock.Instance;
        }

        public IClock Clock { get; set; }

        public async Task<JwtSecurityToken?> VerifyRefreshToken(string refreshToken)
        {
            //SignatureValidation
            var validationParameters = new TokenValidationParameters
            {
                ValidateLifetime = true,
                IssuerSigningKey = new SymmetricSecurityKey(_refreshSecret),
                ValidateAudience = false,
                ValidateIssuer = false
            };
            var handler = new JwtSecurityTokenHandler();
            handler.ValidateToken(refreshToken, validationParameters, out var validToken);


            // Signature validation failed
            if (!(validToken is JwtSecurityToken jwt)) return null;
            var refreshKey = jwt.Claims.SingleOrDefault(c => c.Type == "refresh_key")?.Value;

            return await _refreshTokenRepository.FindByKey(refreshKey ?? "") != null
                ? jwt
                : null;
        }

        public async Task<string> GenerateRefreshToken(string sub)
        {
            //Save new refresh token in db
            var refreshKey = Guid.NewGuid().ToString();
            var expires = Clock.InUtc()
                .GetCurrentInstant()
                .Plus(Duration.FromMinutes(_refreshTimeout));

            var refreshToken = await _refreshTokenRepository.CreateNewToken(refreshKey, sub, expires);

            //refresh claims
            var claims = new List<Claim>
            {
                new Claim("sub", refreshToken.Sub),
                new Claim("refresh_key", refreshKey)
            };

            //generate jwt
            return CreateToken(claims, Clock.InUtc().GetCurrentInstant(), expires, _refreshSecret);
        }

        public string GenerateAccessToken(IEnumerable<Claim> claims)
        {
            var claimList = claims.ToList();
            var expires = Clock.InUtc()
                .GetCurrentInstant()
                .Plus(Duration.FromMinutes(_accessTimeout));

            return CreateToken(claimList, Clock.InUtc().GetCurrentInstant(), expires, _accessSecret);
        }

        private static string CreateToken(IReadOnlyCollection<Claim> claimList, Instant notBefore, Instant expires,
            byte[] secret)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var claimDict = claimList.ToDictionary(c => c.Type, v => (object)v.Value);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claimList),
                Claims = claimDict,
                NotBefore = notBefore.ToDateTimeUtc(),
                Expires = expires.ToDateTimeUtc(),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(secret),
                    SecurityAlgorithms.HmacSha512Signature
                )
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public async Task DestroyRefreshToken(string tokenString)
        {
            var token = await VerifyRefreshToken(tokenString);
            await _refreshTokenRepository.Destroy(token?.Claims?.SingleOrDefault(c => c.Type == "refresh_key")?.Value);
        }
    }
}