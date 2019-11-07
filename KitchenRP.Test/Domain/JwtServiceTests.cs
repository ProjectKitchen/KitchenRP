using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using KitchenRP.DataAccess;
using KitchenRP.DataAccess.Models;
using KitchenRP.Domain.Services;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using Moq;
using NodaTime;
using NodaTime.Testing;
using Xunit;
using Xunit.Sdk;

namespace KitchenRP.Test.Domain
{
    public class JwtServiceTests
    {

        private readonly byte[] _testSecret1 = Encoding.ASCII.GetBytes("testSecret1testSecret1testSecret1testSecret1testSecret1");
        private readonly byte[] _testSecret2 = Encoding.ASCII.GetBytes("testSecret2testSecret2testSecret2testSecret2testSecret2");

        private readonly IKitchenRpDatabase _db;
        private readonly Instant _unixStart = Instant.FromUnixTimeSeconds(0);
        private readonly FakeClock _fromUnixStartOneMinuteInterval;

        private readonly TokenValidationParameters _accessTokenValidationParameters;
        public JwtServiceTests()
        {
            var mockDb = new Mock<IKitchenRpDatabase>();
            mockDb.Setup(db => db.UserGetClaims("testuser1")).Returns(
                Task.FromResult( new List<Claim>
                {
                    new Claim("sub", "testuser1"),
                    new Claim("role", "testrole")
                } as IEnumerable<Claim>));
            
            mockDb.Setup(db => db.UserGetClaims("testuser2")).Returns(
                Task.FromResult( new List<Claim>
                {
                    new Claim("sub", "testuser2"),
                    new Claim("role", "testrole")
                } as IEnumerable<Claim>));
            
            var addedKeys = new List<(string,string)>();
            
            mockDb.Setup(db => db.AddNewRefreshToken(It.IsAny<string>(), It.IsAny<Instant>(), It.IsAny<string>()))
                .Callback<string, Instant, string>((key, exp, sub) =>
                {
                    addedKeys.Remove(addedKeys.Find(a => a.Item1 == sub));
                    addedKeys.Add((sub, key));
                })
                .Returns<string, Instant, string>((key, exp, sub) => Task.FromResult(new RefreshToken
                {
                    Id = 1,
                    Expires = exp,
                    Key = key,
                    Sub = sub
                }));
            
            mockDb.Setup(db => db.IsValidRefreshKey(It.IsIn(addedKeys.Select(tuple => tuple.Item2))))
                .Returns(Task.FromResult(true));
            mockDb.Setup(db => db.IsValidRefreshKey(It.IsNotIn(addedKeys.Select(tuple => tuple.Item2))))
                .Returns(Task.FromResult(false));

            _db = mockDb.Object;
            _fromUnixStartOneMinuteInterval = new FakeClock(_unixStart, Duration.FromMinutes(1));
            
            _accessTokenValidationParameters = new TokenValidationParameters
            {
                ValidateLifetime = true,
                IssuerSigningKey = new SymmetricSecurityKey(_testSecret1),
                ValidateAudience = false,
                ValidateIssuer = false,
                LifetimeValidator = (before, expires, securityToken, parameters) => true
            };

        }
        
        
        [Theory]
        [InlineData(2, "testuser1")]
        [InlineData(3, "testuser2")]
        [InlineData(4, "testuser1")]
        public async void GeneratedAccessTokenContainsCorrectContentAndValidatesAgainstSignature(int tokenOffset1, string user)
        {
            IdentityModelEventSource.ShowPII = true;
            var service = new JwtService(
                _db,
                _testSecret1,
                tokenOffset1,
                _testSecret2,
                -1)
            {
                Clock = _fromUnixStartOneMinuteInterval
            };
            
            var token = service.GenerateAccessToken(await _db.UserGetClaims(user));

            var handler = new JwtSecurityTokenHandler();
            handler.ValidateToken(token, _accessTokenValidationParameters, out var validToken);
            var jwt = validToken as JwtSecurityToken;
            
            Assert.NotNull(jwt);
            Assert.True(Instant.FromDateTimeUtc(jwt.ValidTo)
                .Equals(_unixStart.Plus(Duration.FromMinutes(tokenOffset1))));
            
            Assert.Equal(user, jwt.Subject);
           
            Assert.Equal("testrole", jwt.Claims
                .Where(c => c.Type == "role")
                .Select(c => c.Value).FirstOrDefault());
        }

        [Theory]
        [InlineData(2, "testuser1")]
        [InlineData(100, "testuser2")]
        public async void GeneratedRefreshTokenValidatesAgainstItself(int refreshTimeout, string user)
        {
            var service = new JwtService(
                _db,
                _testSecret1, 
                -1, 
                _testSecret2, 
                refreshTimeout);
            var refreshTokenString = await service.GenerateRefreshToken(user);

            var token = await service.VerifyRefreshToken(refreshTokenString);
            
            Assert.NotNull(token);
            Assert.Equal(user, token.Subject);
            Assert.NotNull(token.Claims.Where(c => c.Type == "refresh_key").Select(c => c.Value).SingleOrDefault());
            Assert.NotEmpty(token.Claims.Where(c => c.Type == "refresh_key").Select(c => c.Value).SingleOrDefault() ?? throw new Exception("Sanity check"));
            
        }
        
        [Theory]
        [InlineData("testuser1")]
        [InlineData("testuser2")]
        public async void RefreshTokenBecomesStale(string user)
        {
            var service = new JwtService(
                _db, 
                _testSecret1, 
                -1, 
                _testSecret2, 
                100);
            
            var refreshTokenString1 = await service.GenerateRefreshToken(user);
            var refreshTokenString2 = await service.GenerateRefreshToken(user);

            var token1 = await service.VerifyRefreshToken(refreshTokenString1);
            Assert.Null(token1);
            
            var token2 = await service.VerifyRefreshToken(refreshTokenString2);
            
            Assert.NotNull(token2);
            Assert.Equal(user, token2.Subject);
            Assert.NotNull(token2.Claims.Where(c => c.Type == "refresh_key").Select(c => c.Value).SingleOrDefault());
            Assert.NotEmpty(token2.Claims.Where(c => c.Type == "refresh_key").Select(c => c.Value).SingleOrDefault() ?? throw new Exception("Sanity check"));
            
        }
        
    }
}