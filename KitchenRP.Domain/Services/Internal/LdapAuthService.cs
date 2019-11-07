using System;
using Novell.Directory.Ldap;

namespace KitchenRP.Domain.Services.Internal
{
    public class LdapAuthService : IAuthenticationService
    {
        private readonly string _ldapHost;
        private readonly ushort _ldapPort;
        private readonly string _searchBase;
        private readonly string _userSearch;

        /// <summary>
        ///     Initializes a AuthService for authentication against a LDAP server
        ///     Note this service uses tls for connecting to the server
        ///     The service will attempt to bind "$userSearch=$username,$searchBase", if successful the user is considered
        ///     authenticated
        /// </summary>
        /// <param name="ldapHost">Ldap Host address</param>
        /// <param name="ldapPort">Ldap server port</param>
        /// <param name="searchBase">Ldap search base</param>
        /// <param name="userSearch">User identifier used for login</param>
        public LdapAuthService(string ldapHost, ushort ldapPort, string searchBase, string userSearch)
        {
            _ldapHost = ldapHost;
            _ldapPort = ldapPort;
            _searchBase = searchBase;
            _userSearch = userSearch;
        }

        /// <summary>
        ///     Authenticates
        ///     <param name="username" />
        ///     against a Ldap server using
        ///     <param name="password"></param>
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns>true if the connection could be successfully bound, false otherwise</returns>
        public bool AuthenticateUser(string username, string password)
        {
            using var connection = new LdapConnection();
            try
            {
                connection.Connect(_ldapHost, _ldapPort);
                connection.StartTls();
                connection.Bind($"{_userSearch}={username},{_searchBase}", password);
            }
            catch (LdapException e)
            {
                Console.WriteLine(e);
                return false;
            }
            finally
            {
                connection.StopTls();
            }

            return true;
        }
    }
}