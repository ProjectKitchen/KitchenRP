using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using KitchenRP.DataAccess;

namespace KitchenRP.Domain.Services
{
    public class KitchenRpAuthorizationService : IAuthorizationService
    {
        public KitchenRpAuthorizationService(IKitchenRpDatabase database)
        {
            _database = database;
        }

        private readonly IKitchenRpDatabase _database;

        public async Task<IEnumerable<Claim>> Authorize(string uid)
        {
            //TODO: make a real implementation
            return await Task.Run(() => new []
            {
                new Claim("sub", "if17b094"), 
                new Claim(ClaimTypes.Role, "admin"),
            });
        }
    }
}