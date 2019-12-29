using System;
using System.Threading.Tasks;
using KitchenRP.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace KitchenRP.Test.IntegrationTests.Database
{
    public class IntegrationDbFixture : IDisposable, IAsyncDisposable
    {
        public IntegrationDbFixture()
        {
            Ctx = new IntegrationDbContext();
            Ctx.Database.EnsureCreated();
        }

        public KitchenRpContext Ctx { get; }

        public void Dispose()
        {
            Ctx.Database.EnsureDeleted();
            Ctx.Dispose();
        }

        public async ValueTask DisposeAsync()
        {
            await Ctx.Database.EnsureDeletedAsync();
            await Ctx.DisposeAsync();
        }
    }
}