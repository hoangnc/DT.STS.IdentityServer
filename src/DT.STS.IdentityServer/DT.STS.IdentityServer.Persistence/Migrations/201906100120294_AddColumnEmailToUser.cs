namespace DT.STS.IdentityServer.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumnEmailToUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.User", "Email", c => c.String(maxLength: 64));
        }
        
        public override void Down()
        {
            DropColumn("dbo.User", "Email");
        }
    }
}
