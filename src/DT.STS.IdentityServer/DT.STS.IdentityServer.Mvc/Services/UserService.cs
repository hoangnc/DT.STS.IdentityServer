using DT.STS.IdentityServer.Application.Permissions.Queries;
using IdentityServer3.Core.Extensions;
using IdentityServer3.Core.Models;
using IdentityServer3.Core.Services;
using IdentityServer3.Core.Services.Default;
using MediatR;
using Microsoft.Owin;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DT.STS.IdentityServer.Mvc.Services
{
    public class UserService : UserServiceBase
    {
        private readonly OwinContext _owinContext;
        private readonly IMediator _mediator;
        public UserService(OwinEnvironmentService owinEnv)
        {
            _mediator = DependencyResolver.Current.GetService<IMediator>();
            _owinContext = new OwinContext(owinEnv.Environment);
        }

        public override Task PreAuthenticateAsync(PreAuthenticationContext context)
        {
            string id = _owinContext.Request.Query.Get("signin");
            context.AuthenticateResult = new AuthenticateResult("~/account/login?id=" + id, (IEnumerable<Claim>)null);
            return Task.FromResult(0);
        }

        public override Task AuthenticateLocalAsync(LocalAuthenticationContext context)
        {
            return base.AuthenticateLocalAsync(context);
        }

        public override async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            Claim userimage = context.Subject.FindFirst(Common.Constants.DtClaimTypes.UserImage);
            List<GetPermissionsByUserAndScopesDto> permissions = await _mediator.Send(new GetPermissionsByUserAndScopesQuery
            {
                Scopes = context.Client.AllowedScopes,
                UserName = context.Subject.GetName()
            });

            List<Claim> claims = new List<Claim>();
            claims.AddRange(context.Subject.Claims);

            if (permissions != null && permissions.Any())
            {
                claims.AddRange(permissions.Select(permission => new Claim($"{permission.ScopeName}_{permission.ClaimName}", permission.ClaimValue)));
            }

            context.IssuedClaims = claims;
        }

        public override Task SignOutAsync(SignOutContext context)
        {
            return base.SignOutAsync(context);
        }
    }
}