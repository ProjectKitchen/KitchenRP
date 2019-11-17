using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using KitchenRP.DataAccess.Models;
using KitchenRP.DataAccess.Repositories;
using KitchenRP.Domain.Commands;
using KitchenRP.Domain.Models;

namespace KitchenRP.Domain.Services.Internal
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _users;

        private static readonly string _userDefaultRole = "user";
        private IMapper _mapper;

        public UserService(IUserRepository users, IMapper mapper)
        {
            _users = users;
            _mapper = mapper;
        }

        public async Task<DomainUser?> UserById(long id)
        {
            var u = await _users.FindById(id);
            return _mapper.Map<User, DomainUser>(u);
        }

        public async Task<IEnumerable<Claim>> GetClaimsForUser(string sub)
        {
            var user = await _users.FindBySub(sub);
            return GenerateClaims(user);
        }


        private static IEnumerable<Claim> GenerateClaims(User user)
        {
            var claims = new List<Claim>
            {
                new Claim("sub", user.Sub),
                new Claim("scope", user.Role.RoleName)
            };
            return claims;
        }

        public async Task<DomainUser?> ActivateNewUser(ActivateUserCommand cmd)
        {
            //TODO: collect Ldap user info
            if (cmd.Email == null)
                throw new NotImplementedException("Automatically fetching emails is currently nor supported");

            return _mapper.Map<User, DomainUser>(await _users.CreateNewUser(cmd.Uid, _userDefaultRole, cmd.Email));
        }
    }
}