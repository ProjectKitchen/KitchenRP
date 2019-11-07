using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace KitchenRP.Domain.Services
{
    public interface IJwtService
    {
        /// <summary>
        ///     Verifies a refresh token
        ///     A refresh token is considered valid if its signature is valid and it is not stale
        ///     A refresh token is considered stale if it was used to refresh an access token or if its validity is expired
        /// </summary>
        /// <param name="refreshToken"></param>
        /// <returns>null if the token was invalid or a token object if valid</returns>
        Task<JwtSecurityToken?> VerifyRefreshToken(string refreshToken);

        /// <summary>
        ///     Generates a new refresh token, for a user with userid
        ///     <param name="sub"></param>
        ///     If a user has an active refresh token the old token will become stale
        /// </summary>
        /// <param name="sub"></param>
        /// <returns>jwt string which can be used as refresh token</returns>
        Task<string> GenerateRefreshToken(string sub);

        /// <summary>
        ///     Generates a new access token with the supplied claims
        ///     The access token can then be used to authenticate and authorize against the api
        /// </summary>
        /// <param name="claims"></param>
        /// <returns>jwt string which can be used as access token</returns>
        string GenerateAccessToken(IEnumerable<Claim> claims);
    }
}