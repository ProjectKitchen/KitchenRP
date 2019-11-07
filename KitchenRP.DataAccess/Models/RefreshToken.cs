using NodaTime;

namespace KitchenRP.DataAccess.Models
{
    public class RefreshToken
    {
        public long Id { get; set; }
        public string Key { get; set; }
        public string Sub { get; set; }
        public Instant Expires { get; set; }
    }
}