// <auto-generated />
namespace DT.STS.IdentityServer.Persistence.Migrations
{
    using System.CodeDom.Compiler;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Migrations.Infrastructure;
    using System.Resources;
    
    [GeneratedCode("EntityFramework.Migrations", "6.2.0-61023")]
    public sealed partial class AddGroupsToUser : IMigrationMetadata
    {
        private readonly ResourceManager Resources = new ResourceManager(typeof(AddGroupsToUser));
        
        string IMigrationMetadata.Id
        {
            get { return "201907260647109_AddGroupsToUser"; }
        }
        
        string IMigrationMetadata.Source
        {
            get { return Resources.GetString("Source"); }
        }
        
        string IMigrationMetadata.Target
        {
            get { return Resources.GetString("Target"); }
        }
    }
}