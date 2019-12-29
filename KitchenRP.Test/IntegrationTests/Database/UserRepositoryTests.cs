using System;
using System.Linq;
using System.Threading.Tasks;
using KitchenRP.DataAccess.Repositories.Internal;
using Xunit;
using Xunit.Abstractions;

namespace KitchenRP.Test.IntegrationTests.Database
{
    [Collection("IntegrationTests")]
    public class UserRepositoryTests : IClassFixture<IntegrationDbFixture>
    {
        public IntegrationDbFixture Fixture;
        private readonly ITestOutputHelper _testOutputHelper;

        public UserRepositoryTests(IntegrationDbFixture fixture, ITestOutputHelper testOutputHelper)
        {
            Fixture = fixture;
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public async Task TestUserManipulation()
        {
            var users = new UserRepository(Fixture.Ctx, new RolesRepository(Fixture.Ctx));
            Assert.Equal(5, Fixture.Ctx.Users.Count());


            var inserted = await users.CreateNewUser("TESTUSER", "user", "TESTEMAIL@TEST.COM");
            Assert.NotNull(inserted.Id);
            Assert.NotNull(inserted.RoleId);

            var foundBySub = await users.FindBySub("TESTUSER");
            var foundById = await users.FindById(inserted.Id.Value);

            Assert.NotNull(foundBySub);
            Assert.NotNull(foundById);
            Assert.Equal(inserted, foundBySub);
            Assert.Equal(inserted, foundById);
            Assert.Equal("TESTUSER", foundById.Sub);
            Assert.Equal("TESTEMAIL@TEST.COM", foundById.Email);
            Assert.True(foundById.AllowNotifications);
        }
    }
}