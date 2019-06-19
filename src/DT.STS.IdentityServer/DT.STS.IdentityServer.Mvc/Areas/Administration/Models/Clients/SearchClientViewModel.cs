namespace DT.STS.IdentityServer.Mvc.Areas.Administration.Models.Clients
{
    public class SearchClientViewModel
    {
        public string ClientId { get; set; }
        public string ClientName { get; set; }
        public string ClientUri { get; set; }
        public string LogoUri { get; set; }
        public bool RequireConsent { get; set; }
        public bool AllowRememberConsent { get; set; }
        /*
         * AuthorizationCode = 0,
        //
        // Summary:
        //     implicit flow
        Implicit = 1,
        //
        // Summary:
        //     hybrid flow
        Hybrid = 2,
        //
        // Summary:
        //     client credentials flow
        ClientCredentials = 3,
        //
        // Summary:
        //     resource owner password credential flow
        ResourceOwner = 4,
        //
        // Summary:
        //     custom grant
        Custom = 5,
        //
        // Summary:
        //     authorization code with proof key flow
        AuthorizationCodeWithProofKey = 6,
        //
        // Summary:
        //     hybrid flow with proof key
        HybridWithProofKey = 7
         */
        public string Flow { get; set; }
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
        /*
         * ReUse = 0,
        //
        // Summary:
        //     Issue a new refresh token handle every time
        OneTimeOnly = 1
         */
        public string RefreshTokenUsage { get; set; }
        /*
         * Sliding = 0,
        //
        // Summary:
        //     Absolute token expiration
        Absolute = 1
         */
        public string RefreshTokenExpiration { get; set; }
        public bool UpdateAccessTokenClaimsOnRefresh { get; set; }
        /*
         * Jwt = 0,
        //
        // Summary:
        //     Reference token
        // Reference = 1
         */
        public string AccessTokenType { get; set; }
        public bool EnableLocalLogin { get; set; }
        public bool IncludeJwtId { get; set; }
        public string AllowedCorsOrigins { get; set; }
        public bool AlwaysSendClientClaims { get; set; }
        public bool PrefixClientClaims { get; set; }
    }
}