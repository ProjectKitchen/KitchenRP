using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using KitchenRP.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace KitchenRP.DataAccess.Repositories.Internal
{
    public class ResourceRepository : IResourceRepository
    {
        private readonly KitchenRpContext _ctx;

        public ResourceRepository(KitchenRpContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<Resource> CreateNewResource(string displayName, JsonDocument metaData, string description,
            string resourceTypeName)
        {
            var resourceType = await FindResourceTypByType(resourceTypeName);
            var resource = new Resource(displayName, metaData, description, resourceType) {IsActive = true};
            _ctx.Resources.Add(resource);
            await _ctx.SaveChangesAsync();
            return resource;
        }

        public Task<Resource> FindById(long id)
        {
            return _ctx.Resources
                .Include(r => r.ResourceType)
                .Where(r =>  r.IsActive)
                .FirstAsync(r => r.Id == id);
        }

        public Task<List<Resource>> All()
        {
            return _ctx.Resources
                .Include(r => r.ResourceType)
                .Where(r =>  r.IsActive)
                .ToListAsync();
        }

        public Task<List<Resource>> ByType(string type)
        {
            var resources = _ctx.Resources
                .Include(r => r.ResourceType)
                .Where(r =>  r.IsActive)
                .Where(r => r.ResourceType.Type == type);

            return resources.ToListAsync();
        }

        public async Task<ResourceType> CreateNewResourceTyp(string type, string resourceName)
        {
            var rt = new ResourceType(type, resourceName);
            _ctx.ResourceTypes.Add(rt);
            await _ctx.SaveChangesAsync();
            return rt;
        }

        public Task<List<ResourceType>> TypeAll()
        {
            return _ctx.ResourceTypes
                .ToListAsync();
        }

        public async Task<ResourceType> FindResourceTypByType(string type)
        {
            var resourceType = await _ctx.ResourceTypes.SingleOrDefaultAsync(r => r.Type == type);
            return resourceType ??
                   throw new EntityNotFoundException(nameof(ResourceType), $"(type == {type})");
        }

        public async Task<Resource> Deactivate(long id)
        {
            var resource = await _ctx.Resources
                .FindAsync(id);
            resource.IsActive = false;
            await _ctx.SaveChangesAsync();
            return resource;
        }
    }
}