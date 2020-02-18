using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace KitchenRP.Domain.Services.Internal
{
    public class KitchenRpAuthorizationService : IAuthorizationService
    {
        private readonly IUserService _users;

        public KitchenRpAuthorizationService(IUserService users)
        {
            _users = users;
        }

        public async Task<IEnumerable<Claim>> Authorize(string uid)
        {
            return await _users.GetClaimsForUser(uid);
        }
    }
}