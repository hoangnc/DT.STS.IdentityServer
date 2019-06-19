using DT.STS.IdentityServer.Config;
using DT.STS.IdentityServer.Services;
using Microsoft.IdentityModel.Protocols;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using Owin;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Linq;
using System.Web.Helpers;
using IdentityServer3.Core;
using IdentityServer3.Core.Configuration;
using IdentityModel.Client;
using System.Threading.Tasks;
using Serilog;
using IdentityServer3.Core.Services;
using Autofac;

[assembly: OwinStartupAttribute(typeof(DT.STS.IdentityServer.Startup))]
namespace DT.STS.IdentityServer
{
    internal class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            AntiForgeryConfig.UniqueClaimTypeIdentifier = Constants.ClaimTypes.Subject;
            JwtSecurityTokenHandler.InboundClaimTypeMap = new Dictionary<string, string>();
            Log.Logger = new LoggerConfiguration()
               .MinimumLevel.Debug()
               .WriteTo.File(@"c:\logs\sts.identityserver.txt")
               .CreateLogger();

            IContainer container = AutofacConfig.ConfigureContainer();
            app.UseAutofacMiddleware(container);

            app.Map("/identity", coreApp =>
            {

                var factory = new IdentityServerServiceFactory()
                    .UseInMemoryClients(Clients.Get())
                    .UseInMemoryScopes(Scopes.Get());
                    //.UseInMemoryUsers(Users.Get());
            

                factory.UserService = new Registration<IUserService, UserService>();

                var options = new IdentityServerOptions
                {
                    SiteName = "Duy Tan Security Token Service",
                    SigningCertificate = Certificate.Get(),
                    Factory = factory,
                    RequireSsl = false,
                    EnableWelcomePage = true,
                    AuthenticationOptions = new IdentityServer3.Core.Configuration.AuthenticationOptions
                    {
                        EnablePostSignOutAutoRedirect = true,
                        EnableSignOutPrompt = true
                    },
                    EventsOptions = new EventsOptions
                    {
                        RaiseSuccessEvents = true,
                        RaiseErrorEvents = true,
                        RaiseFailureEvents = true,
                        RaiseInformationEvents = true
                    }
                };

                coreApp.UseIdentityServer(options);
            });

            app.UseResourceAuthorization(new AuthorizationManager());

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = CookieAuthenticationDefaults.AuthenticationType,
            });

            app.UseOpenIdConnectAuthentication(new OpenIdConnectAuthenticationOptions
            {
                Authority = "http://localhost:58891/identity",
                AuthenticationType = "oidc",
                ClientId = "mvc",
                Scope = "openid profile roles sampleApi",
                ResponseType = "id_token token",
                RedirectUri = "http://localhost:58891",
                // PostLogoutRedirectUri = "http://localhost:58891",
                SignInAsAuthenticationType = CookieAuthenticationDefaults.AuthenticationType,
                UseTokenLifetime = false,
              
                Notifications = new OpenIdConnectAuthenticationNotifications
                {
                    SecurityTokenValidated = async n =>
                    {
                        var nid = new ClaimsIdentity(
                            n.AuthenticationTicket.Identity.AuthenticationType,
                            Constants.ClaimTypes.GivenName,
                            Constants.ClaimTypes.Role);

                        // get userinfo data
                        var userInfoClient = new UserInfoClient(
                            new Uri(n.Options.Authority + "/connect/userinfo"),
                            n.ProtocolMessage.AccessToken);

                        var userInfo = await userInfoClient.GetAsync();
                        userInfo.Claims.ToList().ForEach(ui => nid.AddClaim(new Claim(ui.Item1, ui.Item2)));

                        // keep the id_token for logout
                        nid.AddClaim(new Claim("id_token", n.ProtocolMessage.IdToken));

                        // add access token for sample API
                        nid.AddClaim(new Claim("access_token", n.ProtocolMessage.AccessToken));

                        // keep track of access token expiration
                        nid.AddClaim(new Claim("expires_at", DateTimeOffset.Now.AddSeconds(int.Parse(n.ProtocolMessage.ExpiresIn)).ToString()));

                        // add some other app specific claim
                        nid.AddClaim(new Claim("app_specific", "some data"));

                        n.AuthenticationTicket = new AuthenticationTicket(
                            nid,
                            n.AuthenticationTicket.Properties);
                    },

                    AuthenticationFailed = n => {
                        return Task.FromResult(0);
                    },
                   
                    RedirectToIdentityProvider = n =>
                    {
                        if (n.ProtocolMessage.RequestType == OpenIdConnectRequestType.LogoutRequest)
                        {
                            var idTokenHint = n.OwinContext.Authentication.User.FindFirst("id_token");

                            if (idTokenHint != null)
                            {
                                n.ProtocolMessage.IdTokenHint = idTokenHint.Value;
                            }
                        }

                        return Task.FromResult(0);
                    }
                }
            });
        }
    }
}