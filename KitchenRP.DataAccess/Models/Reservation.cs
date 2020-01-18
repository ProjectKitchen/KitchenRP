using System.Collections;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NodaTime;

namespace KitchenRP.DataAccess.Models
{
    public class Reservation
    {
        public long? Id { get; set; }
        public Instant StartTime { get; set; }
        public Instant EndTime { get; set; }
        public User Owner { get; set; }
        public Resource ReservedResource { get; set; }
        public bool AllowNotifications { get; set; }

        public ICollection<StatusChange> StatusChanges { get; set; }
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