﻿using IdentityServer.WindowsAuthentication.Configuration;
using Microsoft.Owin;
using Owin;
using Serilog;
using System.Configuration;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

[assembly: OwinStartup(typeof(DT.STS.IdentityServer.WinAuth.Startup))]
namespace DT.STS.IdentityServer.WinAuth
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            string idpReplyUrl = ConfigurationManager.AppSettings["IdpReplyUrl"];

            Log.Logger = new LoggerConfiguration()
              .MinimumLevel.Debug()
                .WriteTo.File(@"c:\logs\sts.winauth.txt")
                .CreateLogger();
            //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
           // ServicePointManager.ServerCertificateValidationCallback = delegate (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };

            app.UseWindowsAuthenticationService(new WindowsAuthenticationOptions
            {
                IdpReplyUrl = idpReplyUrl,
                SigningCertificate = Certificate.Get(),
                SubjectType = SubjectType.WindowsAccountName,
                EnableOAuth2Endpoint = false
            });
        }
    }
}