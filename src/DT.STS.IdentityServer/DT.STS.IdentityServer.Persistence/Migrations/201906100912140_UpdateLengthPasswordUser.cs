namespace DT.STS.IdentityServer.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateLengthPasswordUser : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.User", "Password", c => c.String(maxLength: 256));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.User", "Password", c => c.String(maxLength: 20));
        }
    }
}
