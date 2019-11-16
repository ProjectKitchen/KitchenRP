using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KitchenRP.DataAccess.Models;
using KitchenRP.DataAccess.Repositories;
using KitchenRP.Domain.Models;
using KitchenRP.Domain.Models.Messages;


namespace KitchenRP.Domain.Services.Internal
{
    public class ResourceService : IResourceService
    {
        private readonly IResourceRepository _resources;

        public ResourceService(IResourceRepository resources)
        {
            _resources = resources;
        }

        public async Task<DomainResource?> AddNewResource(NewResourceRequest r)
        {
            var resource =
                await _resources.CreateNewResource(r.DisplayName, r.MetaData, r.Description, r.ResourceTypeName);
            return Mapper.Map(resource);
        }

        public async Task<IEnumerable<DomainResource?>> GetAllByType(string type)
        {
            return string.IsNullOrWhiteSpace(type)
                ? (await _resources.All()).Select(Mapper.Map) 
                : (await _resources.ByType(type)).Select(Mapper.Map);
        }

        public async Task<DomainResourceType?> AddNewResourceType(string type, string displayName)
        {
            var resourceTyp = await _resources.CreateNewResourceTyp(type, displayName);
            return Mapper.Map(resourceTyp);
        }

        public async Task<IEnumerable<DomainResourceType?>> GetAllTypes()
        {
            return (await _resources.AllTypes()).Select(Mapper.Map);
        }

        public async Task<DomainResourceType?> GetOneTypeByName(string type)
        {
            return Mapper.Map(await _resources.TypeByType(type));
        }
    }
}