namespace DT.STS.IdentityServer.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddScopesToClient : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Client", "Scopes", c => c.String(maxLength: 4000));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Client", "Scopes");
        }
    }
}
