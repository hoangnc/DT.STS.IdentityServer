using DT.STS.IdentityServer.Application.Clients.Queries;
using DT.STS.IdentityServer.Mvc.Configurations;
using IdentityServer3.Core.Models;
using IdentityServer3.Core.Services;
using MediatR;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace DT.STS.IdentityServer.Mvc.Services
{
    public class ClientStore : IClientStore
    {
        private readonly IMediator _mediator;

        public ClientStore(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<Client> FindClientByIdAsync(string clientId)
        {
            List<Client> defaultClients = new List<Client>();
            object section = ConfigurationManager.GetSection("defaultClientsSection");

            if (section != null)
            {
                DefaultClientSettings clients = (section as DefaultClientsSection).DefaultClientSettings;
                if (clients != null && clients.Count > 0)
                {
                    defaultClients.AddRange(clients.Cast<DefaultClientSetting>()
                        .Select(c =>
                        {
                            if (c.Flow == Flows.ClientCredentials)
                            {
                                return new Client
                                {
                                    ClientId = c.ClientId,
                                    ClientName = c.ClientName,
                                    Flow = c.Flow,
                                    RequireConsent = c.RequireConsent,
                                    ClientSecrets = new List<Secret>
                                    {
                                        new Secret("967ef861-1b5f-4ea1-9fd9-b66c24a8335c".Sha256())
                                    },
                                    AllowedScopes = c.AllowedScopes.Split(';').ToList()
                                };
                            }
                            return new Client
                            {
                                ClientId = c.ClientId,
                                ClientName = c.ClientName,
                                Flow = c.Flow,
                                RequireConsent = c.RequireConsent,
                                RedirectUris = c.RedirectUris.Split(';').ToList(),
                                PostLogoutRedirectUris = c.PostLogoutRedirectUris.Split(';').ToList(),
                                AllowedScopes = c.AllowedScopes.Split(';').ToList(),
                                AllowedCorsOrigins = c.RedirectUris.Split(';').ToList()
                            };
                        })
                    );
                }
            }

            Client client = defaultClients.FirstOrDefault(c => c.ClientId == clientId);
            if (client == null)
            {
                GetClientByClientIdDto dtClient = await _mediator.Send(new GetClientByClientIdQuery
                {
                    ClientId = clientId,
                });

                if (dtClient != null && !string.IsNullOrEmpty(dtClient.ClientId))
                {
                    client = new Client
                    {
                        ClientId = dtClient.ClientId,
                        ClientName = dtClient.ClientName,
                        Flow = dtClient.Flow,
                        RequireConsent = dtClient.RequireConsent,
                        RedirectUris = dtClient.RedirectUris.Split(';').ToList(),
                        AllowedCorsOrigins = dtClient.RedirectUris.Split(';').ToList(),
                        PostLogoutRedirectUris = dtClient.PostLogoutRedirectUris.Split(';').ToList(),
                        AllowedScopes = dtClient.Scopes.Split(';').ToList()/*new List<string> {
                            "openid",
                            "profile",
                            "roles",
                            "documentmvc",
                            "documentapi",
                            "masterdataapi"
                        }*/
                    };
                }
            }

            return client;
        }
    }
}