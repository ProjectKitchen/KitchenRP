using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using KitchenRP.DataAccess.Models;
using KitchenRP.DataAccess.Repositories;
using KitchenRP.Domain.Models;

namespace KitchenRP.Domain.Services.Internal
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _users;

        public UserService(IUserRepository users)
        {
            _users = users;
        }

        public async Task<DomainUser?> UserById(long id)
        {
            var u = await _users.UserById(id);
            return Mapper.Map(u);
        }

        public async Task<IEnumerable<Claim>> GetClaimsForUser(string sub)
        {
            var user = await _users.UserBySub(sub);
            return GenerateClaims(user);
        }


        private static IEnumerable<Claim> GenerateClaims(User user)
        {
            var claims = new List<Claim>
            {
                new Claim("sub", user.Sub),
                new Claim("scope", user.Role.RoleName)
            };
            return claims;
        }
    }
}