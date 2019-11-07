using System.Threading.Tasks;
using KitchenRP.Domain.Services;
using KitchenRP.Domain.Services.Internal;
using Microsoft.AspNetCore.Mvc;

namespace KitchenRP.Web.Controllers
{
    [ApiController]
    [Route("user")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService service)
        {
            _userService = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetById(long id)
        {
            var user = await _userService.UserById(id);
            return Ok(user);
        }
    }
}