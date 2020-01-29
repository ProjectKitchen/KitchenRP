using System.Threading.Tasks;
using AutoMapper;
using KitchenRP.Domain.Commands;
using KitchenRP.Domain.Services;
using KitchenRP.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace KitchenRP.Web.Controllers
{
    [ApiController]
    [Route("resource")]
    public class ResourceController : ControllerBase
    {
        private readonly IResourceService _resourceService;
        private readonly IMapper _mapper;

        public ResourceController(IResourceService resourceService, IMapper mapper)
        {
            _resourceService = resourceService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> AddResource(AddResourceRequest model)
        {
            var resource = await _resourceService.AddNewResource(_mapper.Map<AddResourceCommand>(model));

            return Ok(new AddResourceResponse
            {
                Id = resource.Id,
                Uri = $"resource/{resource.Id}"
            });
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var resources = await _resourceService.GetById(id);
            return Ok(resources);
        }
        

        [HttpGet]
        public async Task<IActionResult> GetByResourceType(string? requestType)
        {
            var resources = await _resourceService.GetAllByType(requestType);
            return Ok(resources);
        }


        [HttpPost]
        [Route("type")]
        public async Task<IActionResult> AddResourceType(AddResourceTypeRequest model)
        {
            var rt = await _resourceService.AddNewResourceType(_mapper.Map<AddResourceTypeCommand>(model));
            return Ok(new AddResourceTypeResponse
            {
                Type = rt.Type,
                Uri = $"resource/type/{rt.Type}"
            });
        }

        [HttpGet]
        [Route("type")]
        public async Task<IActionResult> GetAllResourceTypes()
        {
            var resourceTypes = await _resourceService.GetAllTypes();
            return Ok(resourceTypes);
        }

        
        [HttpDelete]
        [Route("/{id}")]
        public async Task<IActionResult> DeactivateResource(long id)
        {
            await _resourceService.Deactivate(id);
            return NoContent();
        }
        
    }
}