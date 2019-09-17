using Abp.Localization;
using Autofac;
using DT.Core.Web.Common.Identity.Configurations;
using DT.Core.Web.Common.Validation;
using DT.STS.IdentityServer.Mvc.Configurations;
using DT.STS.IdentityServer.Mvc.IdentityServer;
using DT.STS.IdentityServer.Mvc.Services;
using FluentValidation;
using IdentityModel;
using IdentityModel.Client;
using IdentityServer.WindowsAuthentication.Configuration;
using IdentityServer3.Core.Configuration;
using IdentityServer3.Core.Services;
using IdentityServer3.Core.Services.Default;
using Microsoft.IdentityModel.Logging;
using Microsoft.Owin;
using Microsoft.Owin.Host.SystemWeb;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using Microsoft.Owin.Security.WsFederation;
using Owin;
using Serilog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Security;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using IdentityServer3Constants = IdentityServer3.Core.Constants;

[assembly: OwinStartup(typeof(DT.STS.IdentityServer.Mvc.Startup))]

namespace DT.STS.IdentityServer.Mvc
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.RollingFile(HttpContext.Current.Request.PhysicalApplicationPath + @"\logs\sts.identityserver-{Date}.txt")
                .CreateLogger();
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ServicePointManager.ServerCertificateValidationCallback = delegate (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
            AntiForgeryConfig.UniqueClaimTypeIdentifier = IdentityServer3Constants.ClaimTypes.Subject;
            JwtSecurityTokenHandler.InboundClaimTypeMap = new Dictionary<string, string>();
            // Adjust the configuration for anti-CSRF protection to the new unique sub claim type
            JwtSecurityTokenHandler.InboundClaimTypeMap.Clear();

            IContainer container = AutofacConfig.ConfigureContainer();
            app.UseAutofacMiddleware(container);

            CustomLanguageManager customLanguageManager = new CustomLanguageManager(container.Resolve<ILanguageManager>());
            ValidatorOptions.LanguageManager = customLanguageManager;

            app.Use(async (context, next) =>
            {
                try
                {
                    await next();
                }
                catch (Exception ex)
                {
                    Log.Error(ex, "OWIN error.");
                }

            });

            app.Map("/windows", ConfigureWindowsTokenProvider);

            app.Map("/identity", idsrvApp =>
                {
                    idsrvApp.UseAutofacMiddleware(container);
                    IdentityServerServiceFactory factory = new IdentityServerServiceFactory();
                    factory.CustomGrantValidators.Add(new Registration<ICustomGrantValidator>(typeof(WindowsGrantValidator)));
                    factory.UserService = new Registration<IUserService, UserService>();
                    factory.ScopeStore = new Registration<IScopeStore>(container.Resolve<IScopeStore>());
                    factory.ClientStore = new Registration<IClientStore>(container.Resolve<IClientStore>());
                    IdentityModelEventSource.ShowPII = true;
                    IdentityModelEventSource.HeaderWritten = true;

                    var cors = new DefaultCorsPolicyService
                    {
                        AllowAll = true
                    };
                    factory.CorsPolicyService = new Registration<ICorsPolicyService>(cors);

                    //var subjects = X509.LocalMachine.My.SubjectDistinguishedName;

                    IdentityServerOptions options = new IdentityServerOptions
                    {
                        SiteName = "Duy Tan Security Token Service",
                        SigningCertificate = Certificate.Get(),//X509.LocalMachine.My.SubjectDistinguishedName.Find("CN=localhost").First(),//Certificate.Get(),//X509.LocalMachine.My.SerialNumber.Find("CN =*.duytan.com OU=IT O=Duy Tan Plastics Manufacturing Corporation L=Ho Chi Minh C=VN").First(),//Certificate.Get(),
                        Factory = factory,
                        RequireSsl = false,
                        EnableWelcomePage = true,                 
                        AuthenticationOptions = new IdentityServer3.Core.Configuration.AuthenticationOptions
                        {
                            EnablePostSignOutAutoRedirect = true,
                            EnableSignOutPrompt = false,
                        
                        },
                        EventsOptions = new EventsOptions
                        {
                            RaiseSuccessEvents = true,
                            RaiseErrorEvents = true,
                            RaiseFailureEvents = true,
                            RaiseInformationEvents = true
                        },
                        LoggingOptions = new LoggingOptions
                        {
                            EnableWebApiDiagnostics = true,
                            WebApiDiagnosticsIsVerbose = true,
                            EnableHttpLogging = true,
                            EnableKatanaLogging = true
                        }
                    };

                    if (Configs.WindowAuth)
                    {
                        options.AuthenticationOptions = new IdentityServer3.Core.Configuration.AuthenticationOptions
                        {
                            EnablePostSignOutAutoRedirect = true,
                            EnableSignOutPrompt = false,
                            EnableLocalLogin = false,
                            IdentityProviders = ConfigureIdentityProviders,
                            EnableAutoCallbackForFederatedSignout = true,
                        };
                    }

                    idsrvApp.UseIdentityServer(options);
                });

            app.UseResourceAuthorization(new AuthorizationManager());

            app.UseKentorOwinCookieSaver();

            /*app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = "Cookies",
                CookieManager = new SystemWebChunkingCookieManager()

            });*/

            

            /*object section = ConfigurationManager.GetSection("openIdConnectionOptionSection");

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
                        AuthenticationFailed = context =>
                        {
                            context.HandleResponse();
                            context.Response.Redirect("/Error?message=" + context.Exception.Message);
                            return Task.FromResult(0);
                        },
                        MessageReceived = n => {
                            return Task.FromResult(0);
                        },
                        SecurityTokenValidated = async n =>
                            {
                                ClaimsIdentity nid = new ClaimsIdentity(
                                 n.AuthenticationTicket.Identity.AuthenticationType,
                                 ClaimTypes.GivenName,
                                 ClaimTypes.Role);

                                    // get userinfo data
#pragma warning disable CS0618 // Type or member is obsolete
                                    UserInfoClient userInfoClient = new UserInfoClient(
#pragma warning restore CS0618 // Type or member is obsolete
                                n.Options.Authority + "/connect/userinfo");

                                UserInfoResponse userInfo = await userInfoClient.GetAsync(n.ProtocolMessage.AccessToken);
                                userInfo.Claims.ToList().ForEach(ui => nid.AddClaim(new Claim(ui.Type, ui.Value)));

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
                                if (n.ProtocolMessage.RequestType == Microsoft.IdentityModel.Protocols.OpenIdConnectRequestType.LogoutRequest)
                                {
                                    string uri = ConfigurationManager.AppSettings["Host"].ToString();
                                    n.ProtocolMessage.RedirectUri = $"{uri}/";
                                    n.ProtocolMessage.PostLogoutRedirectUri = $"{uri}/";
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
            }*/
        }

        private static void ConfigureWindowsTokenProvider(IAppBuilder app)
        {
            app.UseWindowsAuthenticationService(new WindowsAuthenticationOptions
            {
                IdpReplyUrl = "https://winauth.duytan.com/identity",
                SigningCertificate = Certificate.Get(),
                EnableOAuth2Endpoint = false
            });
        }

        private void ConfigureIdentityProviders(IAppBuilder app, string signInAsType)
        {
            string host = ConfigurationManager.AppSettings["Host"];
            string metadataAddress = ConfigurationManager.AppSettings["MetadataAddress"];
            HttpClientHandler handler;
            handler = new HttpClientHandler();
            //handler.ClientCertificates.Add(X509.LocalMachine.My.SubjectDistinguishedName.Find("CN=localhost").First());
            handler.UseDefaultCredentials = true;
            handler.AllowAutoRedirect = false;
            handler.ServerCertificateCustomValidationCallback =
(req, cert, er, t) =>
true;
            WsFederationAuthenticationOptions wsFederation = new WsFederationAuthenticationOptions
            {
                AuthenticationType = "windows",
                Caption = "Windows",
                SignInAsAuthenticationType = signInAsType,
                MetadataAddress = metadataAddress,
                Wtrealm = "urn:idsrv3",
                BackchannelCertificateValidator = null,
                Notifications = new WsFederationAuthenticationNotifications
                {
                    // ignore signout requests (we can't sign out of Windows)
                    RedirectToIdentityProvider = n =>
                    {
                        if (n.ProtocolMessage.IsSignOutMessage)
                        {
                            // tell IdentityServer to manage the sign out instead of the Windows provider
                            n.OwinContext.Authentication.SignOut();
                            n.HandleResponse();
                        }

                        return Task.FromResult(0);
                    }
                },
                BackchannelHttpHandler =  handler
            };
            app.UseWsFederationAuthentication(wsFederation);
        }

        
    }
    public static class OpenIdConnectAuthenticationPatchedMiddlewareExtension
    {
        public static IAppBuilder UseOpenIdConnectAuthenticationPatched(this IAppBuilder app, OpenIdConnectAuthenticationOptions openIdConnectOptions)
        {
            if (app == null)
            {
                throw new ArgumentNullException("app");
            }
            if (openIdConnectOptions == null)
            {
                throw new ArgumentNullException("openIdConnectOptions");
            }
            Type type = typeof(OpenIdConnectAuthenticationPatchedMiddleware);
            object[] objArray = new object[] { app, openIdConnectOptions };
            return app.Use(type, objArray);
        }
    }

    /// <summary>
    /// Patched to fix the issue with too many nonce cookies described here: https://github.com/IdentityServer/IdentityServer3/issues/1124
    /// Deletes all nonce cookies that weren't the current one
    /// </summary>
    public class OpenIdConnectAuthenticationPatchedMiddleware : OpenIdConnectAuthenticationMiddleware
    {
        private readonly Microsoft.Owin.Logging.ILogger _logger;

        public OpenIdConnectAuthenticationPatchedMiddleware(Microsoft.Owin.OwinMiddleware next, Owin.IAppBuilder app, Microsoft.Owin.Security.OpenIdConnect.OpenIdConnectAuthenticationOptions options)
                : base(next, app, options)
        {
            this._logger = Microsoft.Owin.Logging.AppBuilderLoggerExtensions.CreateLogger<OpenIdConnectAuthenticationPatchedMiddleware>(app);
        }

        protected override Microsoft.Owin.Security.Infrastructure.AuthenticationHandler<OpenIdConnectAuthenticationOptions> CreateHandler()
        {
            return new SawtoothOpenIdConnectAuthenticationHandler(_logger);
        }

        public class SawtoothOpenIdConnectAuthenticationHandler : OpenIdConnectAuthenticationHandler
        {
            public SawtoothOpenIdConnectAuthenticationHandler(Microsoft.Owin.Logging.ILogger logger)
                : base(logger) { }

            /*protected override void RememberNonce(OpenIdConnectMessage message, string nonce)
            {
 
                var oldNonces = Request.Cookies.Where(kvp => kvp.Key.StartsWith(OpenIdConnectAuthenticationDefaults.CookiePrefix + "nonce"));
                if (oldNonces.Any())
                {
                    Microsoft.Owin.CookieOptions cookieOptions = new Microsoft.Owin.CookieOptions
                    {
                        HttpOnly = true,
                        Secure = Request.IsSecure
                    };
                    foreach (KeyValuePair<string, string> oldNonce in oldNonces)
                    {
                        Response.Cookies.Delete(oldNonce.Key, cookieOptions);
                    }
                }
                base.RememberNonce(message, nonce);
            }*/
        }
    }
}