namespace DT.STS.IdentityServer.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDatabaseSchema1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Role",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 128),
                        Description = c.String(maxLength: 1000),
                        UpdatedOn = c.DateTime(),
                        UpdatedBy = c.String(maxLength: 64),
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedBy = c.String(nullable: false, maxLength: 64),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ScopeClaim",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 128),
                        Description = c.String(maxLength: 1000),
                        AlwaysIncludeInIdToken = c.Boolean(nullable: false),
                        UpdatedOn = c.DateTime(),
                        UpdatedBy = c.String(maxLength: 64),
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedBy = c.String(nullable: false, maxLength: 64),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Scope",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Enabled = c.Boolean(nullable: false),
                        Name = c.String(nullable: false, maxLength: 128),
                        DisplayName = c.String(maxLength: 128),
                        Description = c.String(maxLength: 1000),
                        Type = c.Int(nullable: false),
                        Required = c.Boolean(nullable: false),
                        Emphasize = c.Boolean(nullable: false),
                        IncludeAllClaimsForUser = c.Boolean(nullable: false),
                        ClaimsRule = c.String(),
                        ShowInDiscoveryDocument = c.Boolean(nullable: false),
                        UpdatedOn = c.DateTime(),
                        UpdatedBy = c.String(maxLength: 64),
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedBy = c.String(nullable: false, maxLength: 64),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Scope");
            DropTable("dbo.ScopeClaim");
            DropTable("dbo.Role");
        }
    }
}
