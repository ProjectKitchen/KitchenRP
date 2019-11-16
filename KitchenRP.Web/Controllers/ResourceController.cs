using System.Threading.Tasks;
using KitchenRP.DataAccess.Models;
using KitchenRP.Domain.Models;
using KitchenRP.Domain.Models.Messages;
using KitchenRP.Domain.Services;
using KitchenRP.Web.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KitchenRP.Web.Controllers
{
    [ApiController]
    [Microsoft.AspNetCore.Mvc.Route("resource")]
    public class ResourceController: ControllerBase
    {
        private readonly IResourceService _resourceService;

        public ResourceController(IResourceService resourceService)
        {
            _resourceService = resourceService;
        }

        [HttpPost]
        public async Task<IActionResult> AddNewResource(NewResourceRequest model)
        {
            var resource = await _resourceService.AddNewResource(model);
            return Ok(new AddResourceResponse
            {
                Id = resource.Id,
                Uri = $"resource/{resource.Id}"
            });
        }
        
        [HttpGet]
        public async Task<IActionResult> GetByResourceType(string? requestType)
        {
            var resources = await _resourceService.GetAllByType(requestType);
            return Ok(resources);
        }
        
        
        [HttpPost]
        [Microsoft.AspNetCore.Mvc.Route("type")]
        public async Task<IActionResult> AddNewResourceType(NewResourceTypeRequest model)
        {
            var rt = await _resourceService.AddNewResourceType(model.Type, model.DisplayName);
            return Ok(new AddResourceTypeResponse
            {
                Type = rt.Type,
                Uri = $"resource/type/{rt.Type}"
            });
        }
        
        [HttpGet]
        [Microsoft.AspNetCore.Mvc.Route("type")]
        public async Task<IActionResult> GetAllResourceTypes()
        {
            var resourceTypes = await _resourceService.GetAllTypes();
            return Ok(resourceTypes);
        }
        
        [HttpGet]
        [Microsoft.AspNetCore.Mvc.Route("type/{type}")]
        public async Task<IActionResult> GetAllResourceTypes(string type)
        {
            var resourceType = await _resourceService.GetOneTypeByName(type);
            return Ok(resourceType);
        }

    }
}