using IdentityServer3.Core;
using System.Security.Claims;
using System.Security.Principal;

namespace DT.STS.IdentityServer.Mvc.Extensions
{
    public static class IdentityExtensions
    {
        public static string GetUserImage(this IIdentity identity)
        {
            Claim claim = ((ClaimsIdentity)identity).FindFirst(Common.Constants.DtClaimTypes.UserImage);
            return (claim != null) ? claim.Value : string.Empty;
        }

        public static string GetUserEmail(this IIdentity identity)
        {
            Claim claim = ((ClaimsIdentity)identity).FindFirst(Constants.ClaimTypes.Email);
            return (claim != null) ? claim.Value : string.Empty;
        }
    }
}