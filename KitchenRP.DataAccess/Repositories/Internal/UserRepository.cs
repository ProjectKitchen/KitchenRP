using System.Threading.Tasks;
using KitchenRP.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace KitchenRP.DataAccess.Repositories.Internal
{
    public class UserRepository : IUserRepository
    {
        private readonly KitchenRpContext _ctx;

        public UserRepository(KitchenRpContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<User?> UserById(long id)
        {
            return await _ctx.Users
                .Include(u => u.Role)
                .SingleOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User?> UserBySub(string sub)
        {
            return await _ctx.Users.SingleOrDefaultAsync(u => u.Sub == sub);
        }
    }
}