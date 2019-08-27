using DT.STS.IdentityServer.Application.Permissions.Queries;
using DT.STS.IdentityServer.Application.Users.Queries;
using DT.STS.IdentityServer.Mvc.Configurations;
using IdentityServer3.Core.Extensions;
using IdentityServer3.Core.Models;
using IdentityServer3.Core.Services;
using IdentityServer3.Core.Services.Default;
using MediatR;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Mvc;
using IdentityServer3Constants = IdentityServer3.Core.Constants;

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
            if (!Configs.WindowAuth)
            {
                context.AuthenticateResult = new AuthenticateResult("~/account/login?id=" + id, (IEnumerable<Claim>)null);
                return Task.FromResult(0);
            }

            return base.PreAuthenticateAsync(context);
        }

        public override Task PostAuthenticateAsync(PostAuthenticationContext context)
        {
            return base.PostAuthenticateAsync(context);
        }

        public override Task AuthenticateLocalAsync(LocalAuthenticationContext context)
        {
            return base.AuthenticateLocalAsync(context);
        }

        public override async Task AuthenticateExternalAsync(ExternalAuthenticationContext context)
        {
            Claim nameClaim = context.ExternalIdentity.Claims.First(x => x.Type == IdentityServer3Constants.ClaimTypes.Name);
            string userName = "Unknown";
            if (nameClaim != null) userName = nameClaim.Value;
            if (Configs.WindowAuth)
            {
                userName = userName.Split('\\')[1];
            }
            GetUserByUserNameFromAdDto user = await _mediator.Send(new GetUserByUserNameFromAdQuery
            {
                UserName = userName
            });

            string fullName = $"{ user.LastName } { user.FirstName}";
            List<Claim> claims = new List<Claim>();
            string imageBase64String = string.Empty;
            if (user.JpegPhoto != null && user.JpegPhoto.Any())
                claims.Add(new Claim(Common.Constants.DtClaimTypes.UserImage, Convert.ToBase64String(user.JpegPhoto)));

            claims.Add(new Claim(IdentityServer3Constants.ClaimTypes.GivenName, fullName));
            claims.Add(new Claim(IdentityServer3Constants.ClaimTypes.Email, $"{ user.Email}"));

            context.AuthenticateResult = new AuthenticateResult(fullName, userName, claims, identityProvider: context.ExternalIdentity.Provider);
        }

        public override async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            Claim userimage = context.Subject.FindFirst(Common.Constants.DtClaimTypes.UserImage);
            string userName = context.Subject.GetName();

            GetUserByUserNameFromAdDto user = await _mediator.Send(new GetUserByUserNameFromAdQuery
            {
                UserName = userName
            });

            List<GetPermissionsByUserAndScopesDto> permissions = await _mediator.Send(new GetPermissionsByUserAndScopesQuery
            {
                Scopes = context.Client.AllowedScopes,
                UserName = userName
            });

            List<Claim> claims = new List<Claim>();
            claims.AddRange(context.Subject.Claims);//.Where(c => c.Type != IdentityServer3Constants.ClaimTypes.Subject));

            //claims.Add(new Claim(IdentityServer3Constants.ClaimTypes.Subject, $"{user.LastName} {user.FirstName}"));

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