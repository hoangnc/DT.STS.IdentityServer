namespace DT.STS.IdentityServer.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeUpdatedToModifiedColumns : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Department", "ModifiedOn", c => c.DateTime());
            AddColumn("dbo.Department", "ModifiedBy", c => c.String(maxLength: 64));
            AddColumn("dbo.Role", "ModifiedOn", c => c.DateTime());
            AddColumn("dbo.Role", "ModifiedBy", c => c.String(maxLength: 64));
            AddColumn("dbo.ScopeClaim", "ModifiedOn", c => c.DateTime());
            AddColumn("dbo.ScopeClaim", "ModifiedBy", c => c.String(maxLength: 64));
            AddColumn("dbo.Scope", "ModifiedOn", c => c.DateTime());
            AddColumn("dbo.Scope", "ModifiedBy", c => c.String(maxLength: 64));
            AddColumn("dbo.User", "ModifiedOn", c => c.DateTime());
            AddColumn("dbo.User", "ModifiedBy", c => c.String(maxLength: 64));
            DropColumn("dbo.Department", "UpdatedOn");
            DropColumn("dbo.Department", "UpdatedBy");
            DropColumn("dbo.Role", "UpdatedOn");
            DropColumn("dbo.Role", "UpdatedBy");
            DropColumn("dbo.ScopeClaim", "UpdatedOn");
            DropColumn("dbo.ScopeClaim", "UpdatedBy");
            DropColumn("dbo.Scope", "UpdatedOn");
            DropColumn("dbo.Scope", "UpdatedBy");
            DropColumn("dbo.User", "UpdatedOn");
            DropColumn("dbo.User", "UpdatedBy");
        }
        
        public override void Down()
        {
            AddColumn("dbo.User", "UpdatedBy", c => c.String(maxLength: 64));
            AddColumn("dbo.User", "UpdatedOn", c => c.DateTime());
            AddColumn("dbo.Scope", "UpdatedBy", c => c.String(maxLength: 64));
            AddColumn("dbo.Scope", "UpdatedOn", c => c.DateTime());
            AddColumn("dbo.ScopeClaim", "UpdatedBy", c => c.String(maxLength: 64));
            AddColumn("dbo.ScopeClaim", "UpdatedOn", c => c.DateTime());
            AddColumn("dbo.Role", "UpdatedBy", c => c.String(maxLength: 64));
            AddColumn("dbo.Role", "UpdatedOn", c => c.DateTime());
            AddColumn("dbo.Department", "UpdatedBy", c => c.String(maxLength: 64));
            AddColumn("dbo.Department", "UpdatedOn", c => c.DateTime());
            DropColumn("dbo.User", "ModifiedBy");
            DropColumn("dbo.User", "ModifiedOn");
            DropColumn("dbo.Scope", "ModifiedBy");
            DropColumn("dbo.Scope", "ModifiedOn");
            DropColumn("dbo.ScopeClaim", "ModifiedBy");
            DropColumn("dbo.ScopeClaim", "ModifiedOn");
            DropColumn("dbo.Role", "ModifiedBy");
            DropColumn("dbo.Role", "ModifiedOn");
            DropColumn("dbo.Department", "ModifiedBy");
            DropColumn("dbo.Department", "ModifiedOn");
        }
    }
}
