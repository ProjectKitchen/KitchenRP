using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NodaTime;

namespace KitchenRP.DataAccess.Models
{
    public class Reservation
    {
        public Reservation(
            long id,
            Instant startTime,
            Instant endTime,
            User owner,
            Resource reservedResource,
            bool allowNotifications)
        {
            Id = id;
            StartTime = startTime;
            EndTime = endTime;
            Owner = owner;
            ReservedResource = reservedResource;
            AllowNotifications = allowNotifications;
        }

        private Reservation(long id, Instant startTime, Instant endTime, bool allowNotifications)
        {
            Id = id;
            StartTime = startTime;
            EndTime = endTime;
            AllowNotifications = allowNotifications;
        }

        public long Id { get; private set; }
        public Instant StartTime { get; private set; }
        public Instant EndTime { get; private set; }
        public User Owner { get; private set; }
        public Resource ReservedResource { get; private set; }
        public bool AllowNotifications { get; private set; }
    }

    internal class ReservationTypeConfiguration : IEntityTypeConfiguration<Reservation>
    {
        public void Configure(EntityTypeBuilder<Reservation> builder)
        {
            builder
                .HasOne(r => r.Owner)
                .WithMany()
                .HasForeignKey("OwnerId");

            builder
                .HasOne(r => r.ReservedResource)
                .WithMany()
                .HasForeignKey("ReservedResourceId");
        }
    }
}