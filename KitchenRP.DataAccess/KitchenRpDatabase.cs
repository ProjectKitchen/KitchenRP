using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using KitchenRP.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using NodaTime;

namespace KitchenRP.DataAccess
{
    public class KitchenRpDatabase: IKitchenRpDatabase
    {
        private readonly KitchenRpContext _ctx;

        public KitchenRpDatabase(KitchenRpContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<IEnumerable<Claim>> UserGetClaims(string uid)
        {
            var user = await _ctx.Users.Where(u => u.Sub == uid)
                .Include(u => u.Role)
                .FirstOrDefaultAsync();

            return user != null
                ? new[]
                {
                    new Claim("sub", user.Sub),
                    new Claim("scope", user.Role.Role),
                }
                : new Claim[] { };
        }

        public async Task<RefreshToken> AddNewRefreshToken(string tokenKey, Instant expires, string sub)
        {
            _ctx.RefreshTokens.RemoveRange(_ctx.RefreshTokens.Where(t => t.Sub == sub));
            var entry = _ctx.RefreshTokens.Add(new RefreshToken {Expires = expires, Key = tokenKey, Sub = sub});
            await _ctx.SaveChangesAsync();
            return entry.Entity;
        }
        
        public async Task<bool> IsValidRefreshKey(string key)
        {
            return await _ctx.RefreshTokens.Where(r => r.Key == key).CountAsync() != 0;
        }
        
    }
}