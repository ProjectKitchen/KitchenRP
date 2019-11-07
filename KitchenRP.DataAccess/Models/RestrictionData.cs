namespace KitchenRP.DataAccess.Models
{
    public class RestrictionData
    {
        public RestrictionData(
            long id,
            int? maxUsagePerMonthInHours,
            int? maxUsagePerWeekInCount, long restrictionId)
        {
            Id = id;
            MaxUsagePerMonthInHours = maxUsagePerMonthInHours;
            MaxUsagePerWeekInCount = maxUsagePerWeekInCount;
            RestrictionId = restrictionId;
        }

        public long Id { get; private set; }
        public int? MaxUsagePerMonthInHours { get; private set; }
        public int? MaxUsagePerWeekInCount { get; private set; }
        public long RestrictionId { get; private set; }
    }
}