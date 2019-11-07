using System.Reflection;
using KitchenRP.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace KitchenRP.DataAccess
{
    public class KitchenRpContext : DbContext
    {
        internal DbSet<Reservation> Reservations { get; set; }
        internal DbSet<ReservationStatus> ReservationStatuses { get; set; }
        internal DbSet<Resource> Resources { get; set; }
        internal DbSet<ResourceType> ResourceTypes { get; set; }
        internal DbSet<Restriction> Restrictions { get; set; }
        internal DbSet<RestrictionData> RestrictionData { get; set; }
        internal DbSet<StatusChange> StatusChanges { get; set; }
        internal DbSet<User> Users { get; set; }
        internal DbSet<UserRole> UserRoles { get; set; }
        internal DbSet<RefreshToken> RefreshTokens { get; set; }


        public KitchenRpContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSnakeCaseNamingConvention();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ReservationTypeConfiguration());
            modelBuilder.ApplyConfiguration(new StatusChangedTypeConfiguration());
            modelBuilder.ApplyConfiguration(new RestrictionTypeConfiguration());
        }
    }
}