using System.Collections.Generic;
using System.Threading.Tasks;
using KitchenRP.DataAccess.Models;
using KitchenRP.Domain.Models;
using KitchenRP.Domain.Models.Messages;

namespace KitchenRP.Domain.Services
{
    public interface IResourceService
    {
        Task<DomainResource> AddNewResource(NewResourceRequest r);
        Task<IEnumerable<DomainResource>> GetAllByType(string type);
        Task<DomainResourceType> AddNewResourceType(string type, string displayName);
        Task<IEnumerable<DomainResourceType>> GetAllTypes();
        Task<DomainResourceType> GetOneTypeByName(string type);
    }
}