using System;
using System.Text.Json;
using KitchenRP.DataAccess;
using KitchenRP.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace KitchenRP.Test.IntegrationTests.Database
{
    public class IntegrationDbContext : KitchenRpContext
    {
        public IntegrationDbContext() : base(GetOptions())
        {
        }

        private static DbContextOptions GetOptions()
        {
            var connString = "Host=localhost; Port=5432; Database=kitchenrp;User Id=kitchenrp; Password=1234";
            if (string.IsNullOrEmpty(connString))
                throw new Exception(
                    "An Environment variable \"CONNECTION_STRING\" must be specified for integration tests to run!");

            var options = new DbContextOptionsBuilder()
                .UseNpgsql(connString,
                    b => b
                        .MigrationsAssembly("KitchenRP.Web")
                        .UseNodaTime())
                .UseSnakeCaseNamingConvention()
                .EnableSensitiveDataLogging();
            return options.Options;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            AddUsers(modelBuilder);
            AddResourceTypes(modelBuilder);
            AddResources(modelBuilder);
        }

        private static void AddUsers(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasData(new User
                {
                    Id = 1, Sub = "if17b001", Email = "if17b001@technikum-wien.at", AllowNotifications = true,
                    RoleId = 1
                }, new User
                {
                    Id = 2, Sub = "if17b002", Email = "if17b002@technikum-wien.at", AllowNotifications = true,
                    RoleId = 2
                }, new User
                {
                    Id = 3, Sub = "if17b003", Email = "if17b003@technikum-wien.at", AllowNotifications = true,
                    RoleId = 3
                }, new User
                {
                    Id = 4, Sub = "if17b004", Email = "if17b004@technikum-wien.at", AllowNotifications = true,
                    RoleId = 3
                }, new User
                {
                    Id = 5, Sub = "if17b005", Email = "if17b005@technikum-wien.at", AllowNotifications = true,
                    RoleId = 3
                });
        }

        private static void AddResourceTypes(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ResourceType>()
                .HasData(
                    new ResourceType(1, "3D_PRINTER", "3d printer"),
                    new ResourceType(2, "LASERCUTTER", "lasercutter"));
        }

        private static void AddResources(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Resource>()
                .HasData(
                    new Resource
                    {
                        Id = 1, DisplayName = "3D Printer 3001",
                        Description = "A 3d Printer. Lorem ipsum sit dolor ...", MetaData = JsonDocument.Parse("{}"),
                        ResourceTypeId = 1
                    },
                    new Resource
                    {
                        Id = 2, DisplayName = "3D Printer 3002v4",
                        Description = "A 3d Printer. Lorem ipsum sit dolor ...", MetaData = JsonDocument.Parse("{}"),
                        ResourceTypeId = 1
                    },
                    new Resource
                    {
                        Id = 3, DisplayName = "LazorCuttor",
                        Description = "A laser cutter. It cuts lasers. Lorem ipsum sit dolor ...",
                        MetaData = JsonDocument.Parse("{}"), ResourceTypeId = 2
                    },
                    new Resource
                    {
                        Id = 4, DisplayName = "L4Z05CU7705",
                        Description = "A laser cutter. It cuts lasers. Lorem ipsum sit dolor ...",
                        MetaData = JsonDocument.Parse("{}"), ResourceTypeId = 2
                    }
                );
        }
    }
}