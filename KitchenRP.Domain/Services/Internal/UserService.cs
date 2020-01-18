using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using KitchenRP.DataAccess;
using KitchenRP.DataAccess.Models;
using KitchenRP.DataAccess.Repositories;
using KitchenRP.Domain.Commands;
using KitchenRP.Domain.Models;

namespace KitchenRP.Domain.Services.Internal
{
    public class UserService : IUserService
    {
        private const string UserDefaultRole = Roles.User;

        private readonly IUserRepository _users;
        private readonly IRolesRepository _roles;
        private readonly IMapper _mapper;

        public UserService(IUserRepository users,
            IRolesRepository roles,
            IMapper mapper)
        {
            _users = users;
            _roles = roles;
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

            return _mapper.Map<User, DomainUser>(await _users.CreateNewUser(cmd.Uid, UserDefaultRole, cmd.Email));
        }


        public async Task<DomainUser> PromoteUser(PromoteUserCommand cmd)
        {
            var user = await _users.FindById(cmd.Id);
            
            if (user?.Role?.RoleName != Roles.User)
                throw new EntityNotFoundException(nameof(user), $"(id == {cmd.Id} && role == user)");
            
            var modRole = await _roles.FindByRole(Roles.Moderator);
            user.Role = modRole;
            var promoted = await _users.UpdateUser(user);
            return _mapper.Map<DomainUser>(promoted);
        }

        public async Task<DomainUser> DemoteUser(DemoteUserCommand cmd)
        {
            var user = await _users.FindById(cmd.Id);
            
            if (user?.Role?.RoleName != Roles.Moderator)
                throw new EntityNotFoundException(nameof(user), $"(id == {cmd.Id} && role == moderator)");
            
            var modRole = await _roles.FindByRole(Roles.User);
            user.Role = modRole;
            var demoted = await _users.UpdateUser(user);
            return _mapper.Map<DomainUser>(demoted);
        }
    }
}