using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using KitchenRP.DataAccess.Models;
using KitchenRP.DataAccess.Repositories;
using KitchenRP.Domain.Commands;
using KitchenRP.Domain.Models;


namespace KitchenRP.Domain.Services.Internal
{
    public class ResourceService : IResourceService
    {
        private readonly IResourceRepository _resources;
        private IMapper _mapper;

        public ResourceService(IResourceRepository resources, IMapper mapper)
        {
            _resources = resources;
            _mapper = mapper;
        }

        public async Task<DomainResource?> AddNewResource(AddResourceCommand r)
        {
            var resource =
                await _resources.CreateNewResource(r.DisplayName, r.MetaData, r.Description, r.ResourceTypeName);
            return _mapper.Map<DomainResource>(resource);
        }

        public async Task<DomainResource> GetById(long id)
        {
            var resource = await _resources.FindById(id);
            return _mapper.Map<DomainResource>(resource); 
        }

        public async Task<IEnumerable<DomainResource?>> GetAllByType(string type)
        {
            return string.IsNullOrWhiteSpace(type)
                ? (await _resources.All()).Select(_mapper.Map<DomainResource>)
                : (await _resources.ByType(type)).Select(_mapper.Map<DomainResource>);
        }

        public async Task<DomainResourceType?> AddNewResourceType(AddResourceTypeCommand cmd)
        {
            var resourceTyp = await _resources.CreateNewResourceTyp(cmd.Type, cmd.DisplayName);
            return _mapper.Map<DomainResourceType>(resourceTyp);
        }

        public async Task<IEnumerable<DomainResourceType?>> GetAllTypes()
        {
            return (await _resources.TypeAll()).Select(_mapper.Map<DomainResourceType>);
        }

        public async Task<DomainResourceType?> GetOneTypeByName(string type)
        {
            return _mapper.Map<DomainResourceType>(await _resources.FindResourceTypByType(type));
        }

        public async Task Deactivate(long id)
        {
            await _resources.Deactivate(id);
        }
    }
}