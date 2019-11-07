using NodaTime;

namespace KitchenRP.Domain.Models
{
    public class DomainRefreshToken
    {
        internal DomainRefreshToken(string key, string sub, Instant expires)
        {
            Key = key;
            Sub = sub;
            Expires = expires;
        }

        public string Key { get; }
        public string Sub { get; }
        public Instant Expires { get; }
    }
}