using System.Collections.Generic;
using System.Threading.Tasks;
using KitchenRP.DataAccess.Models;
using KitchenRP.Domain.Commands;
using KitchenRP.Domain.Models;

namespace KitchenRP.Domain.Services
{
    public interface IResourceService
    {
        Task<DomainResource> AddNewResource(AddResourceCommand r);
        Task<DomainResource> GetById(long id);
        Task<IEnumerable<DomainResource>> GetAllByType(string type);
        Task<DomainResourceType> AddNewResourceType(AddResourceTypeCommand cmd);
        Task<IEnumerable<DomainResourceType>> GetAllTypes();
        Task<DomainResourceType> GetOneTypeByName(string type);
        Task Deactivate(long id);
    }
}