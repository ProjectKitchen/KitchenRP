using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using KitchenRP.DataAccess.Models;

namespace KitchenRP.DataAccess.Repositories
{
    public interface IResourceRepository
    {
        Task<Resource> CreateNewResource(string displayName, JsonDocument metaData, string description,
            string resourceTypeName);

        Task<Resource> UpdateResource(Resource update);

        Task<List<Resource>> ByType(string type);

        Task<Resource> FindById(long id);

        Task<List<Resource>> All();

        Task<ResourceType> CreateNewResourceTyp(string type, string resourceName);

        Task<List<ResourceType>> TypeAll();

        Task<ResourceType> FindResourceTypByType(string type);

        Task<Resource> Deactivate(long id);
    }
}