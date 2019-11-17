using System.Diagnostics;
using System.Threading.Tasks;
using KitchenRP.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace KitchenRP.DataAccess.Repositories.Internal
{
    public class UserRepository : IUserRepository
    {
        private readonly KitchenRpContext _ctx;
        private readonly IRolesRepository _roles;

        public UserRepository(KitchenRpContext ctx, IRolesRepository roles)
        {
            _ctx = ctx;
            _roles = roles;
        }

        public async Task<User?> FindById(long id)
        {
            return await _ctx.Users
                .Include(u => u.Role)
                .SingleOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User?> FindBySub(string sub)
        {
            return await _ctx.Users.SingleOrDefaultAsync(u => u.Sub == sub);
        }

        public async Task<User> CreateNewUser(string sub, string role, string email)
        {
            var userRole = await _roles.FindByRole(role) ??
                           throw new NotFoundException(nameof(UserRole), $"role = {role}");
            var u = new User
            {
                Sub = sub,
                Email = email,
                AllowNotifications = true,
                Role = userRole
            };
            _ctx.Users.Add(u);
            await _ctx.SaveChangesAsync();
            return u;
        }
    }
}