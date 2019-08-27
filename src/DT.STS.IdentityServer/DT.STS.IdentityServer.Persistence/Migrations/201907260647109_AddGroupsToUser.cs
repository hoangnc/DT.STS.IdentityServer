namespace DT.STS.IdentityServer.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddGroupsToUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.User", "Groups", c => c.String(maxLength: 1024));
        }
        
        public override void Down()
        {
            DropColumn("dbo.User", "Groups");
        }
    }
}
