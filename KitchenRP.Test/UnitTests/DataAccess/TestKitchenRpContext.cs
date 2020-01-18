using KitchenRP.DataAccess;
using KitchenRP.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace KitchenRP.Test.UnitTests.DataAccess
{
    public class TestKitchenRpContext : KitchenRpContext
    {
        public TestKitchenRpContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Resource>()
                .Ignore(r => r.MetaData);

            modelBuilder.Entity<UserRole>()
                .HasData(
                    new UserRole(1, "testRole1"),
                    new UserRole(2, "testRole2"),
                    new UserRole(3, "testRole3")
                );
        }
    }
}