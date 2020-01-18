using System.Threading.Tasks;
using KitchenRP.DataAccess.Models;
using NodaTime;

namespace KitchenRP.DataAccess.Repositories
{
    public interface IRefreshTokenRepository
    {
        Task<RefreshToken> FindByKey(string tokenKey);
        Task<RefreshToken> CreateNewToken(string tokenKey, string sub, Instant expires);
        Task ExpireTokensForUser(string sub);
        Task Destroy(string tokenKey);
    }
}