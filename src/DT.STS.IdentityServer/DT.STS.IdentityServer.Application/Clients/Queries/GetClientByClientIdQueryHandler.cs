using DT.STS.IdentityServer.Persistence;
using MediatR;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DT.STS.IdentityServer.Application.Clients.Queries
{
    public class GetClientByClientIdQueryHandler : IRequestHandler<GetClientByClientIdQuery, GetClientByClientIdDto>
    {
        private readonly STSDbContext _context;
        public GetClientByClientIdQueryHandler(STSDbContext context)
        {
            _context = context;
        }

        public async Task<GetClientByClientIdDto> Handle(GetClientByClientIdQuery request, CancellationToken cancellationToken)
        {
            return await _context.Clients.Where(client => client.ClientId.Equals(request.ClientId))
                .Select(client => new GetClientByClientIdDto
                {
                    Id = client.Id,
                    ClientId = client.ClientId,
                    ClientName = client.ClientName,
                    AbsoluteRefreshTokenLifetime = client.AbsoluteRefreshTokenLifetime,
                    AccessTokenLifetime = client.AccessTokenLifetime,
                    AccessTokenType = client.AccessTokenType,
                    AllowAccessTokensViaBrowser = client.AllowAccessTokensViaBrowser,
                    AllowClientCredentialsOnly = client.AllowClientCredentialsOnly,
                    AllowedCorsOrigins = client.AllowedCorsOrigins,
                    AllowedCustomGrantTypes = client.AllowedCustomGrantTypes,
                    AllowRememberConsent = client.AllowRememberConsent,
                    AlwaysSendClientClaims = client.AlwaysSendClientClaims,
                    AuthorizationCodeLifetime = client.AuthorizationCodeLifetime,
                    ClientUri = client.ClientUri,
                    EnableLocalLogin = client.EnableLocalLogin,
                    Flow = client.Flow,
                    IdentityTokenLifetime = client.IdentityTokenLifetime,
                    IncludeJwtId = client.IncludeJwtId,
                    LogoUri = client.LogoUri,
                    LogoutSessionRequired = client.LogoutSessionRequired,
                    LogoutUri = client.LogoutUri,
                    PostLogoutRedirectUris = client.PostLogoutRedirectUris,
                    PrefixClientClaims = client.PrefixClientClaims,
                    RedirectUris = client.RedirectUris,
                    RefreshTokenExpiration = client.RefreshTokenExpiration,
                    RefreshTokenUsage = client.RefreshTokenUsage,
                    RequireConsent = client.RequireConsent,
                    RequireSignOutPrompt = client.RequireSignOutPrompt,
                    SlidingRefreshTokenLifetime = client.SlidingRefreshTokenLifetime,
                    UpdateAccessTokenClaimsOnRefresh = client.UpdateAccessTokenClaimsOnRefresh,
                   
                }).FirstOrDefaultAsync();
        }
    }
}
