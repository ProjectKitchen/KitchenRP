using KitchenRP.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace KitchenRP.DataAccess
{
    public class KitchenRpContext : DbContext
    {
        public KitchenRpContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<ReservationStatus> ReservationStatuses { get; set; }
        public DbSet<Resource> Resources { get; set; }
        public DbSet<ResourceType> ResourceTypes { get; set; }
        public DbSet<Restriction> Restrictions { get; set; }
        public DbSet<RestrictionData> RestrictionData { get; set; }
        public DbSet<StatusChange> StatusChanges { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSnakeCaseNamingConvention();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(KitchenRpContext).Assembly);

            modelBuilder.Entity<UserRole>()
                .HasData(
                    new UserRole(1, "admin"),
                    new UserRole(2, "moderator"),
                    new UserRole(3, "user")
                );

            modelBuilder.Entity<ReservationStatus>()
                .HasData(
                    new ReservationStatus(1, "PENDING", "Reservation pending ..."),
                    new ReservationStatus(2, "NEEDS_APPROVAL", "Reservation needs approval ..."),
                    new ReservationStatus(3, "DENIED", "Reservation was denied!"),
                    new ReservationStatus(4, "APPROVED", "Reservation was approved!")
                );
        }
    }
}