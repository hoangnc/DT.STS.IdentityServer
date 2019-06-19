namespace DT.STS.IdentityServer.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddScopeScopeClaims : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ScopeScopeClaim",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ScopeId = c.Int(nullable: false),
                        ScopeClaimId = c.Int(nullable: false),
                        ModifiedOn = c.DateTime(),
                        ModifiedBy = c.String(maxLength: 64),
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedBy = c.String(nullable: false, maxLength: 64),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ScopeScopeClaim");
        }
    }
}
