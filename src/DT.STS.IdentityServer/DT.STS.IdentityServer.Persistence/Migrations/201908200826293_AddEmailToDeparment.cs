namespace DT.STS.IdentityServer.Persistence.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddEmailToDeparment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Department", "Email", c => c.String(maxLength: 64));
        }

        public override void Down()
        {
            DropColumn("dbo.Department", "Email");
        }
    }
}
