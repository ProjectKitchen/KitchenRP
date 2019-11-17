using System;
using System.Threading.Tasks;
using KitchenRP.DataAccess;
using KitchenRP.DataAccess.Models;
using KitchenRP.DataAccess.Repositories.Internal;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace KitchenRP.Test.DataAccess
{
    public class UserDataAccessTests
    {
        private readonly DbContextOptions<KitchenRpContext> _options;

        public UserDataAccessTests()
        {
            _options = new DbContextOptionsBuilder<KitchenRpContext>()
                .UseInMemoryDatabase("user_test_db")
                .Options;
        }

        [Theory]
        [InlineData("user1", "testRole1", "user1@mail.com")]
        [InlineData("user2", "testRole2", "user1@mail.com")]
        [InlineData("user3", "testRole3", null)]
        public async Task CreateUserOfUserNotInSystemShouldWork(string userName, string userRole, string userEmail)
        {
            User? enteredUser = null;
            User? returnedUser = null;
            await using (var ctx = new TestKitchenRpContext(_options))
            {
                ctx.Database.EnsureCreated();
                var users = new UserRepository(ctx, new RolesRepository(ctx));
                enteredUser = await users.CreateNewUser(userName, userRole, userEmail);
            }

            await using (var ctx = new TestKitchenRpContext(_options))
            {
                var users = new UserRepository(ctx, new RolesRepository(ctx));
                returnedUser = await users.FindById(enteredUser.Id);
            }

            Assert.NotNull(returnedUser);

            Assert.Equal(returnedUser.Id, enteredUser.Id);
            Assert.Equal(returnedUser.Email, enteredUser.Email);
            Assert.Equal(returnedUser.Sub, enteredUser.Sub);
            Assert.Equal(returnedUser.AllowNotifications, enteredUser.AllowNotifications);
            Assert.Equal(returnedUser.Role?.Id, enteredUser.Role?.Id);

            Assert.Equal(returnedUser.Email, userEmail);
            Assert.Equal(returnedUser.Sub, userName);
            Assert.Equal(returnedUser.Role?.RoleName, userRole);
        }

        [Theory]
        [InlineData("a", "NonExistingRole1", "a@a.a")]
        [InlineData("a", "NonExistingRole2", "a@a.a")]
        [InlineData("a", "NonExistingRole3", null)]
        public async Task CreateUserOfUserWithNotExistingRoleShouldThrow(string userName, string userRole,
            string userEmail)
        {
            await using (var ctx = new TestKitchenRpContext(_options))
            {
                ctx.Database.EnsureCreated();
                var users = new UserRepository(ctx, new RolesRepository(ctx));
                await Assert.ThrowsAsync<NotFoundException>(async () =>
                    await users.CreateNewUser(userName, userRole, userEmail));
            }
        }
    }
}