using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using KitchenRP.Domain.Commands;
using KitchenRP.Domain.Models;

namespace KitchenRP.Domain.Services
{
    public interface IUserService
    {
        /// <summary>
        ///     Returns a list of Claims for the supplied User
        ///     Guarantied to return at least 'sub' and 'role' Claim
        /// </summary>
        /// <param name="sub"></param>
        /// <returns></returns>
        Task<IEnumerable<Claim>> GetClaimsForUser(string sub);

        /// <summary>
        /// Returns a user by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<DomainUser?> UserById(long id);
        
        Task<DomainUser?> UserByName(string name);

        /// <summary>
        ///     Activates the given uid for this application
        ///     A user is then considered activated and may login to use this service
        ///     If no email is supplied the default fh email is fetched from the ldap server
        /// </summary>
        /// <param name="uid">fh uid</param>
        /// <param name="email">optional email address</param>
        /// <returns>New activated user object</returns>
        Task<DomainUser?> ActivateNewUser(ActivateUserCommand cmd);
        Task<DomainUser> PromoteUser(PromoteUserCommand cmd);
        Task<DomainUser> DemoteUser(DemoteUserCommand cmd);
    }
}