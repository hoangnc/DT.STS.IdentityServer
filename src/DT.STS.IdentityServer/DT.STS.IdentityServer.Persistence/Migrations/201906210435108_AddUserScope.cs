namespace DT.STS.IdentityServer.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserScope : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserScopes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Users = c.String(),
                        ScopeName = c.String(),
                        ModifiedOn = c.DateTime(),
                        ModifiedBy = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UserScopes");
        }
    }
}
