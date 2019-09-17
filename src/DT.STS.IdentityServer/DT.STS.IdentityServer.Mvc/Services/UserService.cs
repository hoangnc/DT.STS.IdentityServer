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
using IdentityModel.Client;
using IdentityServer3Constants = IdentityServer3.Core.Constants;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using System.Text;
using IdentityModel;
using System.Security.Cryptography.X509Certificates;
using System.IdentityModel.Tokens;
using DT.STS.IdentityServer.Mvc.IdentityServer;
using System.Configuration;
using IdentityServer3.Core.Logging;
using DT.Core.Text;
using System.Threading;

namespace DT.STS.IdentityServer.Mvc.Services
{
    public class LoggingHandler : DelegatingHandler
    {
        private readonly static ILog Logger = LogProvider.For<LoggingHandler>();
        public LoggingHandler(HttpMessageHandler innerHandler)
            : base(innerHandler)
        {
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            Logger.Info("Request get win token:");
            Logger.Info(request.ToString());
            if (request.Content != null)
            {
                Logger.Info(await request.Content.ReadAsStringAsync());
            }

            HttpResponseMessage response = await base.SendAsync(request, cancellationToken);

            Logger.Info("Response get win token:");
            Logger.Info(response.ToString());
            if (response.Content != null)
            {
                Logger.Info(await response.Content.ReadAsStringAsync());
            }

            return response;
        }
    }
    public class UserService : UserServiceBase
    {
        private readonly OwinContext _owinContext;
        private readonly IMediator _mediator;
        private string Host => ConfigurationManager.AppSettings["Host"];
        private string WinAuth => ConfigurationManager.AppSettings["MetadataAddress"];
        private readonly static ILog Logger = LogProvider.For<UserService>();
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
            }
            /*else
            {
                var handler = new HttpClientHandler
                {
                    UseDefaultCredentials = true,
                };
#pragma warning disable CS0618 // Type or member is obsolete
                var client = new TokenClient(
#pragma warning restore CS0618 // Type or member is obsolete
                //"https://localhost:443/token",
                $"{WinAuth}/token",
                new LoggingHandler(handler));

                var resultToken = await client.RequestCustomGrantAsync("windows");

                Logger.Info($"Result token: {resultToken.Error}");
                Logger.Info($"Result token: {resultToken.ErrorDescription}");

                if (!resultToken.AccessToken.IsNullOrEmpty() 
                    && resultToken.AccessToken.Contains("."))
                {

                    var parts = resultToken.AccessToken.Split('.');
                    var header = parts[0];
                    var claims = parts[1];

                    Console.WriteLine(JObject.Parse(Encoding.UTF8.GetString(Base64Url.Decode(header))));
                    Console.WriteLine(JObject.Parse(Encoding.UTF8.GetString(Base64Url.Decode(claims))));

#pragma warning disable CS0618 // Type or member is obsolete
                    var clientForAuth = new TokenClient(
#pragma warning restore CS0618 // Type or member is obsolete
                      $"{Host}/identity/connect/token");

                    Logger.Info("Test:" + JObject.Parse(Encoding.UTF8.GetString(Base64Url.Decode(header))).ToString());
                    Logger.Info("Test" + JObject.Parse(Encoding.UTF8.GetString(Base64Url.Decode(claims))).ToString());
                    var additionalvalues = new Dictionary<string, string>()
                    {
                        { "client_id", "dtwinauthclient" },
                        { "client_secret", "secret" },
                        { "win_token", resultToken.AccessToken }
                    };

                    var authResult = await clientForAuth.RequestCustomGrantAsync("windows", "openid profile roles", additionalvalues);// profile roles admindocumentmvc admindocumentapi documentmvc documentapi masterdataapi
                    var principal = TryValidateToken(authResult.AccessToken, Certificate.Get());
                    context.AuthenticateResult = new AuthenticateResult(principal.GetSubjectId(), principal.GetSubjectId(), principal.Claims);
                }
                else
                {
                    context.AuthenticateResult = new AuthenticateResult("~/account/login?id=" + id, (IEnumerable<Claim>)null);
                }
            }*/
            //return base.PreAuthenticateAsync(context);
            return Task.FromResult(0);
        }

       /* private ClaimsPrincipal TryValidateToken(string token, X509Certificate2 signingCert)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenValidationParams = new TokenValidationParameters
            {
                ValidAudience = $"{Host}/identity/resources",
                ValidIssuer = $"{Host}/identity",
                IssuerSigningKey = new X509SecurityKey(signingCert)
            };

            SecurityToken securityToken;
            var claimsPrincipial = tokenHandler.ValidateToken(token, tokenValidationParams, out securityToken);

            return claimsPrincipial;
        }*/

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
            claims.Add(new Claim(IdentityServer3Constants.ClaimTypes.Name, $"{ userName}"));

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