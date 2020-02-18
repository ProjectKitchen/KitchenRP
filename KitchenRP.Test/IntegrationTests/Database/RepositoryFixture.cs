using System;
using System.Threading.Tasks;
using KitchenRP.DataAccess.Repositories;
using KitchenRP.DataAccess.Repositories.Internal;

namespace KitchenRP.Test.IntegrationTests.Database
{
    public class RepositoryFixture : IDisposable, IAsyncDisposable
    {
        public RepositoryFixture()
        {
            Database = new IntegrationDbFixture();

            RefreshTokens = new RefreshTokenRepository(Database.Ctx);
            Reservations = new ReservationRepository(Database.Ctx);
            ReservationStatuses = new ReservationStatusRepository(Database.Ctx);
            Resources = new ResourceRepository(Database.Ctx);
            Roles = new RolesRepository(Database.Ctx);
            Users = new UserRepository(Database.Ctx, Roles);
        }

        public IntegrationDbFixture Database { get; }

        public IRefreshTokenRepository RefreshTokens { get; }
        public IReservationRepository Reservations { get; }
        public IReservationStatusRepository ReservationStatuses { get; }
        public IResourceRepository Resources { get; }
        public IRolesRepository Roles { get; }
        public IUserRepository Users { get; }

        public void Dispose()
        {
            Database.Dispose();
        }

        public ValueTask DisposeAsync()
        {
            return Database.DisposeAsync();
        }
    }
}