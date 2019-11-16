using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace KitchenRP.Web
{
    public static class Errors
    {

        public static ProblemDetails EntityNotFound(string entity, string query = "(?)")
        {
            return new ProblemDetails
            {
                Type = "EntityNotFound",
                Title = $"Could not find Entity {entity}",
                Detail = $"Unable to find entity {entity} with query: {query}",
                Status = 404,
            };
        }
        
        public static ProblemDetails NotYetRegisteredError()
        {
            return new ProblemDetails
            {
                Type = "NotYetRegistered",
                Title = "Account is not registered",
                Detail = "Your credentials seems valid, but your account is not yet registered with ProjectKitchen.\n" +
                         "Only ProjectKitchen staff can activate your account.",
                Status = 403
            };
        }

        public static ProblemDetails InvalidCredentials()
        {
            return new ProblemDetails
            {
                Type = "InvalidCredentials",
                Title = "Invalid credentials",
                Detail = "Username or password were invalid. Make sure you use your usual university login",
                Status = 403
            };
        }

        public static ProblemDetails BadRefreshToken()
        {
            return new ProblemDetails
            {
                Type = "BadRefreshToken",
                Title = "Bad refresh token",
                Detail =
                    "Provided refresh token could not be verified, this could be because the token expired, or because it was revoked",
                Status = 400
            };
        }
    }
}