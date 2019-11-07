using System.Linq;
using System.Threading.Tasks;
using KitchenRP.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using NodaTime;

namespace KitchenRP.DataAccess.Repositories.Internal
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly KitchenRpContext _ctx;

        public RefreshTokenRepository(KitchenRpContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<RefreshToken> GetForKey(string tokenKey)
        {
            return await _ctx.RefreshTokens.SingleOrDefaultAsync(t => t.Key == tokenKey);
        }

        public async Task<RefreshToken> CreateNewToken(string tokenKey, string sub, Instant expires)
        {
            var token = new RefreshToken
            {
                Expires = expires, Key = tokenKey, Sub = sub
            };
            _ctx.RefreshTokens.Add(token);
            await _ctx.SaveChangesAsync();
            return token;
        }

        public async Task ExpireTokensForUser(string sub)
        {
            _ctx.RefreshTokens.RemoveRange(_ctx.RefreshTokens.Where(t => t.Sub == sub));
            await _ctx.SaveChangesAsync();
        }
    }
}