using IdentityServer3.Core.Models;
using System.Collections.Generic;

namespace DT.STS.IdentityServer.Mvc.IdentityServer
{
    public static class Clients
    {
        public static IEnumerable<Client> Get()
        {
            return new[]
            {
                new Client 
                {
                    ClientName = "IdentityServer Client",
                    ClientId = "idserver",
                    Flow = Flows.Implicit,
                    RequireConsent = false,
                    RedirectUris = new List<string>
                    {
                        "https://localhost:44319/",
                        "https://localhost:44319/administration/dashboard"
                    },
                    PostLogoutRedirectUris = new List<string>
                    {
                        "https://localhost:44319/"
                    },
                    AllowedScopes = new List<string>
                    {
                        "openid",
                        "profile",
                        "roles",
                        "adminapi"
                    },
                },
                new Client
                {
                    ClientName = "IdentityServer (service communication)",   
                    ClientId = "idserver_service",
                    Flow = Flows.ClientCredentials,

                    ClientSecrets = new List<Secret>
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes = new List<string>
                    {
                        "adminapi"
                    }
                }
            };
        }
    }
}