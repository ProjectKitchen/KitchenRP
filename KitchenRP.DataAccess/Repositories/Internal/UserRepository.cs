using System;
using System.Collections.Generic;
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
                .Where(u => u.IsActive)
                .Include(u => u.Role)
                .SingleOrDefaultAsync(u => u.Id == id);
        }

        public Task<List<User>> GetAll()
        {
            return _ctx.Users
                .Where(u => u.IsActive)
                .Include(u => u.Role)
                .ToListAsync();
        }

        public async Task<User> FindBySub(string sub)
        {
            return await _ctx.Users
                .Where(u => u.IsActive)
                .Include(u => u.Role)
                .SingleOrDefaultAsync(u => u.Sub == sub);
        }

        public async Task<User> CreateNewUser(string sub, string role, string email)
        {
            var userRole = await _roles.FindByRole(role) ??
                           throw new NotFoundException(nameof(UserRole), $"role = {role}");
            var exists = await Exists(sub);
            
            if (exists)
            {
                throw new Exception("User already exists");
            }
            
            var u = new User
            {
                Sub = sub,
                Email = email,
                AllowNotifications = true,
                Role = userRole,
                IsActive = true
            };
            _ctx.Users.Add(u);
            await _ctx.SaveChangesAsync();
            return u;
        }
        
        public async Task<User> UpdateUser(User update)
        {
            var user = await _ctx.Users
                .Where(u => u.IsActive && update.Id == u.Id)
                .FirstOrDefaultAsync();
            user.AllowNotifications = update.AllowNotifications;
            user.Email = update.Email;
            user.Role = update.Role;
            await _ctx.SaveChangesAsync();
            return user;
        }
        
        public async Task<bool> Exists(string sub)
        {
            return await _ctx.Users
                       .Where(u => u.IsActive)
                       .CountAsync(u => u.Sub == sub) > 0;
        }

        public async Task<User> RemoveUser(long id)
        {
            var user = await _ctx.Users.FindAsync(id);
            user.IsActive = false;
            await _ctx.SaveChangesAsync();
            return user;
        }
    }
}