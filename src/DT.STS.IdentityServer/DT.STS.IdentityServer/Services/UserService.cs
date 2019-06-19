using IdentityServer3.Core;
using IdentityServer3.Core.Extensions;
using IdentityServer3.Core.Models;
using IdentityServer3.Core.Services;
using IdentityServer3.Core.Services.Default;
using Microsoft.Owin;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DT.STS.IdentityServer.Services
{
    public class UserService : UserServiceBase
    {
        private OwinContext ctx;
        public UserService(OwinEnvironmentService owinEnv)
        {
            ctx = new OwinContext(owinEnv.Environment);
        }

        public override Task PreAuthenticateAsync(PreAuthenticationContext context)
        {
            string id = ctx.Request.Query.Get("signin");
            context.AuthenticateResult = new AuthenticateResult("~/account/login?id=" + id, (IEnumerable<Claim>)null);
            return Task.FromResult(0);
        }

        public override Task AuthenticateLocalAsync(LocalAuthenticationContext context)
        {
            return base.AuthenticateLocalAsync(context);
        }

        public override Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var userimage = context.Subject.FindFirst(Common.Constants.DtClaimTypes.UserImage);
            
            context.IssuedClaims = new Claim[]
            {
                 new Claim(Constants.ClaimTypes.GivenName, context.Subject.GetSubjectId())
            };

            return Task.FromResult(0);
        }

        public override Task SignOutAsync(SignOutContext context)
        {
            return base.SignOutAsync(context);
        }
    }
}