namespace DT.STS.IdentityServer.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddClientAndUpdatePrimaryKeyScope : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Client",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClientId = c.String(nullable: false, maxLength: 128),
                        ClientName = c.String(nullable: false, maxLength: 256),
                        ClientUri = c.String(nullable: false, maxLength: 256),
                        LogoUri = c.String(maxLength: 256),
                        RequireConsent = c.Boolean(nullable: false),
                        AllowRememberConsent = c.Boolean(nullable: false),
                        Flow = c.Int(nullable: false),
                        AllowClientCredentialsOnly = c.Boolean(nullable: false),
                        RedirectUris = c.String(maxLength: 4000),
                        PostLogoutRedirectUris = c.String(maxLength: 4000),
                        LogoutUri = c.String(maxLength: 256),
                        LogoutSessionRequired = c.Boolean(nullable: false),
                        RequireSignOutPrompt = c.Boolean(nullable: false),
                        AllowAccessTokensViaBrowser = c.Boolean(nullable: false),
                        AllowedCustomGrantTypes = c.Boolean(nullable: false),
                        IdentityTokenLifetime = c.Int(nullable: false),
                        AccessTokenLifetime = c.Int(nullable: false),
                        AuthorizationCodeLifetime = c.Int(nullable: false),
                        AbsoluteRefreshTokenLifetime = c.Int(nullable: false),
                        SlidingRefreshTokenLifetime = c.Int(nullable: false),
                        RefreshTokenUsage = c.Int(nullable: false),
                        RefreshTokenExpiration = c.Int(nullable: false),
                        UpdateAccessTokenClaimsOnRefresh = c.Boolean(nullable: false),
                        AccessTokenType = c.Int(nullable: false),
                        EnableLocalLogin = c.Boolean(nullable: false),
                        IncludeJwtId = c.Boolean(nullable: false),
                        AllowedCorsOrigins = c.String(),
                        AlwaysSendClientClaims = c.Boolean(nullable: false),
                        PrefixClientClaims = c.Boolean(nullable: false),
                        ModifiedOn = c.DateTime(),
                        ModifiedBy = c.String(maxLength: 64),
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedBy = c.String(nullable: false, maxLength: 64),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.Id, t.ClientId });
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Client");
        }
    }
}
