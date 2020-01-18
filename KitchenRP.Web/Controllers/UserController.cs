using System;
using System.Threading.Tasks;
using AutoMapper;
using KitchenRP.Domain.Commands;
using KitchenRP.Domain.Services;
using KitchenRP.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace KitchenRP.Web.Controllers
{
    [ApiController]
    [Route("user")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService service, IMapper mapper)
        {
            _userService = service;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetByUsername(string username)
        {
            var user = await _userService.UserByName(username);
            return Ok(_mapper.Map<UserResponse>(user));
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
            return Ok(_mapper.Map<UserResponse>(user));
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
            var user = await _userService.ActivateNewUser(_mapper.Map<ActivateUserCommand>(model));

            if (user == null) return this.Error(Errors.UnableToActivateUser(model!.Uid));

            var uri = $"user/{user.Id}";
            return Created(uri,
                new UserActivationResponse
                {
                    Id = user.Id,
                    Uri = uri
                });
        }

        
        [HttpPut]
        [Route("/{id}/promote")]
        public async Task<IActionResult> PromoteUser(long id)
        {
            var promotedUser = await _userService.PromoteUser(new PromoteUserCommand{Id = id});
            return NoContent();
        }
        
        [HttpPut]
        [Route("/{id}/demote")]
        public async Task<IActionResult> DemoteUser(long id)
        {
            var demotedUser = await _userService.DemoteUser(new DemoteUserCommand{Id = id});
            return NoContent();
        }
        
        
    }
}