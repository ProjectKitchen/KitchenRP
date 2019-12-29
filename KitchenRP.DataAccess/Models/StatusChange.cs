using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NodaTime;

namespace KitchenRP.DataAccess.Models
{
    public class StatusChange
    {
        public long? Id { get; set; }
        public ReservationStatus PreviousStatus { get; set; }
        public ReservationStatus CurrentStatus { get; set; }
        public string Reason { get; set; }
        public Instant ChangedAt { get; set; }
        public long? ReservationId { get; set; }
        public Reservation Reservation { get; set; }
        public User ChangedBy { get; set; }
    }

    internal class StatusChangedTypeConfiguration : IEntityTypeConfiguration<StatusChange>
    {
        public void Configure(EntityTypeBuilder<StatusChange> builder)
        {
            builder.HasOne(s => s.PreviousStatus)
                .WithMany()
                .HasForeignKey("PreviousStatusID");

            builder.HasOne(s => s.CurrentStatus)
                .WithMany()
                .HasForeignKey("CurrentStatusId");

            builder.HasOne(s => s.ChangedBy)
                .WithMany()
                .HasForeignKey("ChangeByUserId");

            builder.HasOne<Reservation>(sc => sc.Reservation)
                .WithMany(r => r.StatusChanges)
                .HasForeignKey(sc => sc.ReservationId)
                .HasPrincipalKey(r => r.Id)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}