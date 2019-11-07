using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NodaTime;

namespace KitchenRP.DataAccess.Models
{
    public class StatusChange
    {
        public StatusChange(
            long id,
            ReservationStatus previousStatus,
            ReservationStatus currentStatus,
            string reason,
            Instant changedAt,
            Reservation reservation,
            User changedBy)
        {
            Id = id;
            PreviousStatus = previousStatus;
            CurrentStatus = currentStatus;
            Reason = reason;
            ChangedAt = changedAt;
            Reservation = reservation;
            ChangedBy = changedBy;
        }

        public StatusChange(long id, string reason, Instant changedAt)
        {
            Id = id;
            Reason = reason;
            ChangedAt = changedAt;
        }

        public long Id { get; private set; }
        public ReservationStatus PreviousStatus { get; private set; }
        public ReservationStatus CurrentStatus { get; private set; }
        public string Reason { get; private set; }
        public Instant ChangedAt { get; private set; }
        public Reservation Reservation { get; private set; }
        public User ChangedBy { get; private set; }
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
        }
    }
}