using System;

namespace KitchenRP.Web.Models
{
    public class NewTokenResponse
    {
        public NewTokenResponse(string accessToken, string refreshToken, DateTime iat)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
            Iat = iat;
        }

        public string AccessToken { get; }
        public string RefreshToken { get; }
        public DateTime Iat { get; }
    }
}