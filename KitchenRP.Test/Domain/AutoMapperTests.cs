using AutoMapper;
using KitchenRP.DataAccess.Models;
using KitchenRP.Domain.Models;
using Xunit;

namespace KitchenRP.Test.Domain
{
    public class AutoMapperTests
    {
        [Fact]
        public void AutoMapperConfigurationIsValid()
        {
            var cfg = new MapperConfiguration(
                cfgOpts => cfgOpts.AddProfile(new KitchenRpDomainModelMappings()));
            cfg.AssertConfigurationIsValid();
        }

        [Fact]
        public void TestUserMappings()
        {
            var mapper = new MapperConfiguration(
                    cfgOpts => cfgOpts.AddProfile(new KitchenRpDomainModelMappings()))
                .CreateMapper();

            var user = new User
            {
                AllowNotifications = true,
                Email = "email",
                Id = 1,
                Sub = "abc",
                Role = new UserRole(1, "role")
            };

            var domainUser = mapper.Map<User, DomainUser>(user);

            Assert.Equal(domainUser.Email, user.Email);
            Assert.Equal(domainUser.Id, user.Id);
            Assert.Equal(domainUser.Role, user.Role.RoleName);
            Assert.Equal(domainUser.Sub, user.Sub);
            Assert.Equal(domainUser.AllowNotifications, user.AllowNotifications);
        }

        [Fact]
        public void TestResourceMappings()
        {
            var mapper = new MapperConfiguration(
                    cfgOpts => cfgOpts.AddProfile(new KitchenRpDomainModelMappings()))
                .CreateMapper();


            var resource = new Resource(
                1, "", null, "", new ResourceType(1, "", "")
            );

            var domainUser = mapper.Map<Resource, DomainResource>(resource);

            Assert.Equal(domainUser.Description, resource.Description);
            Assert.Equal(domainUser.Id, resource.Id);
            Assert.Equal(domainUser.DisplayName, resource.DisplayName);
            Assert.Equal(domainUser.MetaData, resource.MetaData);
            Assert.Equal(domainUser.ResourceType.Type, resource.ResourceType.Type);
        }
    }
}