namespace KitchenRP.Domain.Services
{
    public interface IAuthenticationService
    {
        /// <summary>
        /// Authenticates a user based on username and password
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        bool AuthenticateUser(string username, string password);
    }
}