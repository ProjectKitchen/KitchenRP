using System;
using System.Linq;
using System.Threading.Tasks;
using KitchenRP.Domain.Models;
using KitchenRP.Domain.Services;
using KitchenRP.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using IAuthorizationService = KitchenRP.Domain.Services.IAuthorizationService;

namespace KitchenRP.Web.Controllers
{
    [ApiController]
    [Route("token")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IAuthorizationService _authorizationService;
        private readonly IJwtService _tokenService;

        public AuthController(
            IAuthenticationService authenticationService,
            IAuthorizationService authorizationService,
            IJwtService tokenService)
        {
            _authorizationService = authorizationService;
            _authenticationService = authenticationService;
            _tokenService = tokenService;
        }

        /// <summary>
        ///     Generates a new access and refresh token, id the supplied refresh token is valid
        ///     after use the old refresh token is considered stale and therefore invalid
        /// </summary>
        /// <param name="model">A valid refresh token</param>
        /// <returns>a TokenResponse with a new access and refresh token</returns>
        [HttpPost("refresh")]
        [AllowAnonymous]
        public async Task<IActionResult> RefreshAuth(RefreshAccessRequest model)
        {
            var token = await _tokenService.VerifyRefreshToken(model.RefreshToken!);
            if (token == null) return this.Error(Errors.BadRefreshToken());

            var sub = token.Subject;
            var claims = (await _authorizationService.Authorize(sub)).ToList();

            var newAccessToken = _tokenService.GenerateAccessToken(claims);
            var newRefreshToken = await _tokenService.GenerateRefreshToken(sub);

            return Ok(new NewTokenResponse(newAccessToken, newRefreshToken, DateTime.Now));
        }

        /// <summary>
        ///     Tries to authenticate a user based on the provided credentials
        ///     A user will be successfully logged in if they provide valid fh credentials AND if their account is enabled by an
        ///     admin
        /// </summary>
        /// <param name="model"></param>
        /// <returns>
        ///     an access and refresh token
        /// </returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Authenticate(AuthRequest model)
        {
            //if authentication fails their credentials are wrong or no longer a valid fh account
            if (!_authenticationService.AuthenticateUser(model.Username!, model.Password!))
                return this.Error(Errors.InvalidCredentials());

            var claims = (await _authorizationService.Authorize(model.Username!)).ToList();

            //if authentication succeeds but authorization does not, their account was not cleared by an admin
            if (!claims.Any()) return this.Error(Errors.NotYetRegisteredError());

            //both succeed -> generate tokens based on claims
            var accessToken = _tokenService.GenerateAccessToken(claims);
            var refreshToken = await _tokenService.GenerateRefreshToken(model.Username!);

            return Ok(new NewTokenResponse(accessToken, refreshToken, DateTime.UtcNow));
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpGet("tokenTest")]
        public IActionResult Test()
        {
            return Ok(User.Claims.Where(c => c.Type == "role"));
        }
    }
}