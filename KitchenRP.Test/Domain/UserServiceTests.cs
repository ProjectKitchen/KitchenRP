using System.Threading.Tasks;
using AutoMapper;
using KitchenRP.DataAccess;
using KitchenRP.DataAccess.Models;
using KitchenRP.DataAccess.Repositories;
using KitchenRP.Domain.Commands;
using KitchenRP.Domain.Models;
using KitchenRP.Domain.Services.Internal;
using Moq;
using Xunit;

namespace KitchenRP.Test.Domain
{
    public class UserServiceTests
    {
        
        private readonly Mock<IUserRepository> _mockUsers = new Mock<IUserRepository>();
        private readonly Mock<IRolesRepository> _mockRoles = new Mock<IRolesRepository>();
        private Mock<IMapper> _mockMapper = new Mock<IMapper>();
        
        private IUserRepository _users;
        private IRolesRepository _roles;
        private IMapper _mapper;
        
        public UserServiceTests()
        {
            _mockUsers.Setup(users => users.FindById(1))
                .Returns(Task.FromResult(new User
                {
                    AllowNotifications = true,
                    Email = "email",
                    Id = 1,
                    Role = new UserRole(1, "user"),
                    Sub = "sub"
                }));
            _mockUsers.Setup(users => users.FindById(2))
                .Returns(Task.FromResult(new User
                {
                    AllowNotifications = true,
                    Email = "email",
                    Id = 2,
                    Role = new UserRole(2, "moderator"),
                    Sub = "sub"
                }));
            
            
            _mockRoles.Setup(roles => roles.FindByRole("user"))
                .ReturnsAsync(new UserRole(1, "user"));
            _mockRoles.Setup(roles => roles.FindByRole("moderator"))
                .ReturnsAsync(new UserRole(2, "moderator"));
            _mockRoles.Setup(roles => roles.FindByRole("admin"))
                .ReturnsAsync(new UserRole(3, "admin"));

            _mockUsers.Setup(users => users.UpdateUser(It.IsAny<User>()))
                .Returns<User>(Task.FromResult);

            _mockMapper.Setup(mapper => mapper.Map<DomainUser>(It.IsAny<User>()))
                .Returns<User>(u => new DomainUser(u.Id.Value, u.Sub, u.Role.RoleName, u.Email, u.AllowNotifications));
            
            _users = _mockUsers.Object;
            _roles = _mockRoles.Object;
            _mapper = _mockMapper.Object;
            
        }
        
        [Fact]
        public async void TestPromoteUserWithRoleUser()
        {
            var service = new UserService(_users, _roles, _mapper);
            var promotionRequest = new PromoteUserCommand {Id = 1};
            var r = await service.PromoteUser(promotionRequest);
            Assert.Equal("moderator", r.Role );
        }
        
        [Fact]
        public void TestPromoteUserWithRoleModeratorFails()
        {
            var service = new UserService(_users, _roles, _mapper);
            var promotionRequest = new PromoteUserCommand {Id = 2};
            Assert.ThrowsAsync<EntityNotFoundException>( async () => await service.PromoteUser(promotionRequest));
        }
        
        [Fact]
        public async void TestDemoteUserWithRoleModerator()
        {
            var service = new UserService(_users, _roles, _mapper);
            var promotionRequest = new DemoteUserCommand {Id = 2};
            var r = await service.DemoteUser(promotionRequest);
            Assert.Equal("user", r.Role );
        }
        
        [Fact]
        public void TestPromoteUserWithRoleUserFails()
        {
            var service = new UserService(_users, _roles, _mapper);
            var promotionRequest = new DemoteUserCommand {Id = 1};
            Assert.ThrowsAsync<EntityNotFoundException>( async () => await service.DemoteUser(promotionRequest));
        }
    }
}