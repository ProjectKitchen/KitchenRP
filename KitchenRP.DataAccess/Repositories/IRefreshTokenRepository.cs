using System.Threading.Tasks;
using KitchenRP.DataAccess.Models;
using NodaTime;

namespace KitchenRP.DataAccess.Repositories
{
    public interface IRefreshTokenRepository
    {
        Task<RefreshToken> GetForKey(string tokenKey);
        Task<RefreshToken> CreateNewToken(string tokenKey, string sub, Instant expires);
        Task ExpireTokensForUser(string sub);
    }
}