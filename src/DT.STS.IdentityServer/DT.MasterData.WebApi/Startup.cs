using Autofac;
using IdentityServer3.AccessTokenValidation;
using IdentityServer3.Core;
using Microsoft.Owin;
using Owin;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Web.Helpers;

[assembly: OwinStartup(typeof(DT.MasterData.WebApi.Startup))]

namespace DT.MasterData.WebApi
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ServicePointManager.ServerCertificateValidationCallback = delegate (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
            AntiForgeryConfig.UniqueClaimTypeIdentifier = Constants.ClaimTypes.Subject;
            JwtSecurityTokenHandler.InboundClaimTypeMap = new Dictionary<string, string>();
            // Adjust the configuration for anti-CSRF protection to the new unique sub claim type
            JwtSecurityTokenHandler.InboundClaimTypeMap.Clear();

            IContainer container = AutofacConfig.ConfigureContainer();
            app.UseAutofacMiddleware(container);


            app.UseIdentityServerBearerTokenAuthentication(new IdentityServerBearerTokenAuthenticationOptions
            {
                Authority = "https://localhost:44319/identity",
                RequiredScopes = new[] { "documentapi" },
            });
        }
    }
}
