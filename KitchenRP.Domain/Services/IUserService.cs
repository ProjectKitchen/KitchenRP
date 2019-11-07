using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using KitchenRP.Domain.Models;

namespace KitchenRP.Domain.Services
{
    public interface IUserService
    {
        Task<IEnumerable<Claim>> GetClaimsForUser(string sub);
        Task<DomainUser> UserById(long id);
    }
}