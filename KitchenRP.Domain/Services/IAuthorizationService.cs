using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace KitchenRP.Domain.Services
{
    
    public interface IAuthorizationService
    {
        /// <summary>
        /// Authorizes a user based on their username
        /// </summary>
        /// <param name="uid"></param>
        /// <returns>A list of claims for this user (e.g: {("sub": "if17b094"), ("role":"user"), ...} )</returns>
        Task<IEnumerable<Claim>> Authorize(string uid);
    }
}