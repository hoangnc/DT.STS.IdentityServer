namespace DT.STS.IdentityServer.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPasswordColumnToUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.User", "Password", c => c.String(maxLength: 20));
        }
        
        public override void Down()
        {
            DropColumn("dbo.User", "Password");
        }
    }
}
