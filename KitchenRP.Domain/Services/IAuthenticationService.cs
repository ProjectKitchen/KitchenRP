using KitchenRP.Domain.Commands;

namespace KitchenRP.Domain.Services
{
    public interface IAuthenticationService
    {
        /// <summary>
        ///     Authenticates a user based on username and password
        /// </summary>
        /// <param name="cmd">Authentication command with username and password</param>
        /// <returns></returns>
        bool AuthenticateUser(AuthCommand cmd);
    }
}