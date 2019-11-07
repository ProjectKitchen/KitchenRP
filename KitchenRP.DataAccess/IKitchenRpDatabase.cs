using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using KitchenRP.DataAccess.Models;
using NodaTime;

namespace KitchenRP.DataAccess
{
    public interface IKitchenRpDatabase
    {
        Task<bool> IsValidRefreshKey(string key);
        Task<RefreshToken> AddNewRefreshToken(string tokenKey, Instant expires, string sub);
        Task<IEnumerable<Claim>> UserGetClaims(string uid);

    }
}