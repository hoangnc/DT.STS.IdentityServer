using System.Security.Claims;
using System.Security.Principal;

namespace DT.STS.IdentityServer.Mvc.Extensions
{
    public static class IdentityExtensions
    {
        public static string GetUserImage(this IIdentity identity)
        {
            Claim claim = ((ClaimsIdentity)identity).FindFirst(Common.Constants.DtClaimTypes.UserImage);
            // Test for null to avoid issues during local testing
            return (claim != null) ? claim.Value : string.Empty;
        }
    }
}