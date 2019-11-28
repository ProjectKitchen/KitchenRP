using System.Diagnostics;
using System.Linq;
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

        public async Task<User> FindById(long id)
        {
            return await _ctx.Users
                .Include(u => u.Role)
                .SingleOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User> FindBySub(string sub)
        {
            return await _ctx.Users
                .Include(u => u.Role)
                .SingleOrDefaultAsync(u => u.Sub == sub);
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
        
        public async Task<User> UpdateUser(User u)
        {
            var user = await _ctx.Users.FindAsync(u.Id);
            user.AllowNotifications = u.AllowNotifications;
            user.Email = u.Email;
            user.Role = u.Role;
            await _ctx.SaveChangesAsync();
            return user;
        }
        
        public async Task<bool> Exists(string sub)
        {
            return await _ctx.Users.CountAsync(u => u.Sub == sub) > 0;
        }
    }
}