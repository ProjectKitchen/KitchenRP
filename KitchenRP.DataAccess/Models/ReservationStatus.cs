using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KitchenRP.DataAccess.Models
{
    public class ReservationStatus
    {
        public ReservationStatus(long id, string status, string displayName)
        {
            Id = id;
            Status = status;
            DisplayName = displayName;
        }

        public long Id { get; private set; }

        public string Status { get; private set; }

        public string DisplayName { get; private set; }
    }

    internal class ReservationStatusEntityConfiguration : IEntityTypeConfiguration<ReservationStatus>
    {
        public void Configure(EntityTypeBuilder<ReservationStatus> builder)
        {
            builder
                .HasData(
                    new ReservationStatus(1, "PENDING", "Reservation pending ..."),
                    new ReservationStatus(2, "NEEDS_APPROVAL", "Reservation needs approval ..."),
                    new ReservationStatus(3, "DENIED", "Reservation was denied!"),
                    new ReservationStatus(4, "APPROVED", "Reservation was approved!")
                );
        }
    }
}