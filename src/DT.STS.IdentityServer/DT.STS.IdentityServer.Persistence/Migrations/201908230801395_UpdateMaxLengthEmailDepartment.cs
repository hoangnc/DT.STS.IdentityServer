namespace DT.STS.IdentityServer.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateMaxLengthEmailDepartment : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Department", "Email", c => c.String(maxLength: 4000));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Department", "Email", c => c.String(maxLength: 64));
        }
    }
}
