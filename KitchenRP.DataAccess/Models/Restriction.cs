using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NodaTime;

namespace KitchenRP.DataAccess.Models
{
    public class Restriction
    {
        public Restriction(
            long id,
            Instant restrictFrom,
            Instant restrictTo,
            bool ignoreYear,
            Resource restrictedResource,
            string displayError,
            RestrictionData data)
        {
            Id = id;
            RestrictFrom = restrictFrom;
            RestrictTo = restrictTo;
            IgnoreYear = ignoreYear;
            RestrictedResource = restrictedResource;
            DisplayError = displayError;
            Data = data;
        }

        private Restriction(long id, Instant restrictFrom, Instant restrictTo, bool ignoreYear, string displayError)
        {
            Id = id;
            RestrictFrom = restrictFrom;
            RestrictTo = restrictTo;
            IgnoreYear = ignoreYear;
            DisplayError = displayError;
        }

        public long Id { get; private set; }
        public Instant RestrictFrom { get; private set; }
        public Instant RestrictTo { get; private set; }
        public bool IgnoreYear { get; private set; }
        public Resource RestrictedResource { get; private set; }

        public RestrictionData Data { get; private set; }
        public string DisplayError { get; private set; }
    }

    internal class RestrictionTypeConfiguration : IEntityTypeConfiguration<Restriction>
    {
        public void Configure(EntityTypeBuilder<Restriction> builder)
        {
            builder
                .HasOne(r => r.Data)
                .WithOne()
                .HasForeignKey<RestrictionData>(rd => rd.RestrictionId);
        }
    }
}