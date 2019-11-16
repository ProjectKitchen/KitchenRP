using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using KitchenRP.DataAccess.Models;

namespace KitchenRP.DataAccess.Repositories
{
    public interface IResourceRepository
    {
        
        Task<ResourceType> CreateNewResourceTyp(string type, string resourceName);
        Task<List<Resource>> ByType(string type);
        Task<List<Resource>> All();
        Task<List<ResourceType>> AllTypes();
        Task<ResourceType> TypeByType(string type);

        Task<Resource> CreateNewResource(string displayName, JsonDocument metaData, string description,
            string resourceTypeName);
    }
}