

using System.Security.Claims;
using System.Security.Principal;

namespace DT.STS.IdentityServer.Extensions
{
    public static class IdentityExtensions
    {
        public static string GetUserImage(this IIdentity identity)
        {
            Claim claim = ((ClaimsIdentity)identity).FindFirst("UserImage");
            // Test for null to avoid issues during local testing
            return (claim != null) ? claim.Value : string.Empty;
        }
    }
}