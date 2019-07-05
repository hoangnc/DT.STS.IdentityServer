using Autofac;
using DT.Core.Web.Common.Identity.Configurations;
using DT.STS.IdentityServer.Mvc.IdentityServer;
using DT.STS.IdentityServer.Mvc.Services;
using IdentityModel.Client;
using IdentityServer3.Core;
using IdentityServer3.Core.Configuration;
using IdentityServer3.Core.Services;
using Microsoft.IdentityModel.Protocols;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Microsoft.Owin.Security.OpenIdConnect;
using Owin;
using Serilog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Web.Helpers;

[assembly: OwinStartup(typeof(DT.STS.IdentityServer.Mvc.Startup))]

namespace DT.STS.IdentityServer.Mvc
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File(@"c:\logs\sts.identityserver.txt")
                .CreateLogger();
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ServicePointManager.ServerCertificateValidationCallback = delegate (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
            AntiForgeryConfig.UniqueClaimTypeIdentifier = Constants.ClaimTypes.Subject;
            JwtSecurityTokenHandler.InboundClaimTypeMap = new Dictionary<string, string>();
            // Adjust the configuration for anti-CSRF protection to the new unique sub claim type
            JwtSecurityTokenHandler.InboundClaimTypeMap.Clear();

            IContainer container = AutofacConfig.ConfigureContainer();
            app.UseAutofacMiddleware(container);

            app.Map("/identity", idsrvApp =>
                {
                    idsrvApp.UseAutofacMiddleware(container);
                    IdentityServerServiceFactory factory = new IdentityServerServiceFactory()
                    .UseInMemoryClients(Clients.Get());

                    factory.UserService = new Registration<IUserService, UserService>();
                    factory.ScopeStore = new Registration<IScopeStore>(container.Resolve<IScopeStore>());
                    factory.ClientStore = new Registration<IClientStore>(container.Resolve<IClientStore>());
                    IdentityServerOptions options = new IdentityServerOptions
                    {
                        SiteName = "Duy Tan Security Token Service",
                        SigningCertificate = Certificate.Get(),
                        Factory = factory,
                        RequireSsl = false,
                        EnableWelcomePage = true,
                        AuthenticationOptions = new IdentityServer3.Core.Configuration.AuthenticationOptions
                        {
                            EnablePostSignOutAutoRedirect = true,
                            EnableSignOutPrompt = false
                        },
                        EventsOptions = new EventsOptions
                        {
                            RaiseSuccessEvents = true,
                            RaiseErrorEvents = true,
                            RaiseFailureEvents = true,
                            RaiseInformationEvents = true
                        }
                    };

                    idsrvApp.UseIdentityServer(options);
                });

            /*app.Map("/masterdatas", idsrvApp =>
            {
                idsrvApp.UseIdentityServerBearerTokenAuthentication(new IdentityServerBearerTokenAuthenticationOptions
                {
                    Authority = "https://localhost:44319/identity",
                    RequiredScopes = new[] { "documentapi" },
                });
            });*/


            app.UseResourceAuthorization(new AuthorizationManager());

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = "Cookies"
            });

            object section = ConfigurationManager.GetSection("openIdConnectionOptionSection");

            if (section != null)
            {
                OpenIdConnectionOption openIdConnectionOption = (section as OpenIdConnectionOptionSection).OpenIdConnectionOption;
                app.UseOpenIdConnectAuthentication(new OpenIdConnectAuthenticationOptions
                {
                    Authority = openIdConnectionOption.Authority,
                    ClientId = openIdConnectionOption.ClientId,
                    Scope = openIdConnectionOption.Scopes,
                    ResponseType = openIdConnectionOption.ResponseType,
                    RedirectUri = openIdConnectionOption.RedirectUri,
                    SignInAsAuthenticationType = openIdConnectionOption.SignInAsAuthenticationType,
                    UseTokenLifetime = openIdConnectionOption.UseTokenLifetime,
                    Notifications = new OpenIdConnectAuthenticationNotifications
                    {
                        SecurityTokenValidated = async n =>
                            {
                                ClaimsIdentity nid = new ClaimsIdentity(
                                    n.AuthenticationTicket.Identity.AuthenticationType,
                                    Constants.ClaimTypes.GivenName,
                                    Constants.ClaimTypes.Role);

                                    // get userinfo data
                                    UserInfoClient userInfoClient = new UserInfoClient(
                                        new Uri(n.Options.Authority + "/connect/userinfo"),
                                        n.ProtocolMessage.AccessToken);

                                UserInfoResponse userInfo = await userInfoClient.GetAsync();
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

                        RedirectToIdentityProvider = n =>
                            {
                                if (n.ProtocolMessage.RequestType == OpenIdConnectRequestType.LogoutRequest)
                                {
                                    Claim idTokenHint = n.OwinContext.Authentication.User.FindFirst("id_token");

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

        private void ConfigureIdentityProviders(IAppBuilder app, string signInAsType)
        {
            app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions
            {
                AuthenticationType = "Google",
                Caption = "Sign-in with Google",
                SignInAsAuthenticationType = signInAsType,
                ClientId = "701386055558-9epl93fgsjfmdn14frqvaq2r9i44qgaa.apps.googleusercontent.com",
                ClientSecret = "3pyawKDWaXwsPuRDL7LtKm_o"
            });
        }
    }
}