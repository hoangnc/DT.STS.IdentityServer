namespace DT.STS.IdentityServer.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddClaimAndUserClaim : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Claim",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 128),
                        Value = c.String(nullable: false, maxLength: 128),
                        ModifiedOn = c.DateTime(),
                        ModifiedBy = c.String(maxLength: 64),
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedBy = c.String(nullable: false, maxLength: 64),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.Id, t.Name, t.Value });
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Claim");
        }
    }
}
