using System;
using System.Threading.Tasks;
using KitchenRP.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace KitchenRP.DataAccess.Repositories.Internal
{
    public class RolesRepository : IRolesRepository
    {
        public RolesRepository(KitchenRpContext ctx)
        {
            _ctx = ctx;
        }

        private KitchenRpContext _ctx;

        public async Task<UserRole> FindByRole(string role)
        {
            var userRole = await _ctx.UserRoles.SingleOrDefaultAsync(r => role == r.RoleName);
            return userRole;
        }
    }
}