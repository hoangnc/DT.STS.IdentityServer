namespace DT.STS.IdentityServer.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Department",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(nullable: false, maxLength: 64),
                        Name = c.String(nullable: false, maxLength: 64),
                        UpdatedOn = c.DateTime(),
                        UpdatedBy = c.String(maxLength: 64),
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedBy = c.String(nullable: false, maxLength: 64),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false, maxLength: 64),
                        FirstName = c.String(maxLength: 64),
                        LastName = c.String(maxLength: 64),
                        FullNameUnicode = c.String(maxLength: 64),
                        LastLoginDate = c.DateTime(),
                        Active = c.Boolean(nullable: false),
                        JpegPhoto = c.Binary(),
                        ManagerName = c.String(maxLength: 64),
                        DepartmentName = c.String(),
                        DirectReports = c.String(),
                        UpdatedOn = c.DateTime(),
                        UpdatedBy = c.String(maxLength: 64),
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedBy = c.String(nullable: false, maxLength: 64),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.User");
            DropTable("dbo.Department");
        }
    }
}
