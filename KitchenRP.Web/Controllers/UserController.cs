using System.Threading.Tasks;
using KitchenRP.Domain.Services;
using KitchenRP.Domain.Services.Internal;
using KitchenRP.Web.Models;
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
        /// <summary>
        ///     Gets a specific user data by their id.
        ///     If the requested user corresponds to the current no special role is required,
        ///     otherwise only admin roles may access others user data
        /// </summary>
        /// <param name="id">Id of the requested user</param>
        /// <returns>User data</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var user = await _userService.UserById(id);
            return Ok(Mapper.Map(user));
        }
        /// <summary>
        ///     Activates a user
        ///     After this action is successful, they may login with their fh credentials and use this service.
        ///     Needs Admin role 
        /// </summary>
        /// <param name="model">fh userid and optional email, if no email is provided the fh email is discovered from the ldap server</param>
        /// <returns>URI of the created user resource and user ID</returns>
        [HttpPost]
        public async Task<IActionResult> ActivateUser(UserActivationRequest model)
        {
            var user = await _userService.ActivateNewUser(model!.Uid, model.Email);
            
            if(user == null) return this.Error(Errors.UnableToActivateUser(model.Uid));
            var uri = $"user/{user.Id}";
            return Created(uri,
                new UserActivationResponse
                {
                    Id = user.Id,
                    Uri = uri
                });
        }
    }
}