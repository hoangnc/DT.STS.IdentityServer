using IdentityServer3.Core.Models;

namespace DT.STS.IdentityServer.Domain.Entities
{
    public class Client : BaseEntity
    {
        public string ClientId { get; set; }
        public string ClientName { get; set; }
        public string ClientUri { get; set; }
        public string LogoUri { get; set; }
        public bool RequireConsent { get; set; }
        public bool AllowRememberConsent { get; set; }
        public Flows Flow { get; set; }
        public bool AllowClientCredentialsOnly { get; set; }
        public string RedirectUris { get; set; }
        public string PostLogoutRedirectUris { get; set; }
        public string LogoutUri { get; set; }
        public bool LogoutSessionRequired { get; set; }
        public bool RequireSignOutPrompt { get; set; }
        public bool AllowAccessTokensViaBrowser { get; set; }
        public bool AllowedCustomGrantTypes { get; set; }
        public int IdentityTokenLifetime { get; set; }
        public int AccessTokenLifetime { get; set; }
        public int AuthorizationCodeLifetime { get; set; }
        public int AbsoluteRefreshTokenLifetime { get; set; }
        public int SlidingRefreshTokenLifetime { get; set; }
        public TokenUsage RefreshTokenUsage { get; set; }
        public TokenExpiration RefreshTokenExpiration { get; set; }
        public bool UpdateAccessTokenClaimsOnRefresh { get; set; }
        public AccessTokenType AccessTokenType { get; set; }
        public bool EnableLocalLogin { get; set; }
        public bool IncludeJwtId { get; set; }
        public string AllowedCorsOrigins { get; set; }
        public bool AlwaysSendClientClaims { get; set; }
        public bool PrefixClientClaims { get; set; }
    }
}
