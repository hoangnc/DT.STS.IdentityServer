using DT.STS.IdentityServer.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DT.STS.IdentityServer.Persistence.Configurations
{
    public class ScopeConfiguration : EntityTypeConfiguration<Scope>
    {
        public ScopeConfiguration()
        {
            HasKey(e => new { e.Id, e.Name });

            Property(e => e.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            ToTable("Scope");

            Property(e => e.Name).IsRequired()
                .HasMaxLength(128);

            Property(e => e.Description)
               .HasMaxLength(1000);

            Property(e => e.DisplayName)
                .HasMaxLength(128);

            Property(e => e.Enabled).IsRequired();

            Property(e => e.Type).IsRequired();

            Property(e => e.IncludeAllClaimsForUser).IsRequired();

            Property(e => e.ShowInDiscoveryDocument).IsRequired();

            Property(e => e.ClaimsRule);

            Property(e => e.CreatedBy).IsRequired()
             .HasMaxLength(64);
            Property(e => e.CreatedOn).IsRequired();

            Property(e => e.ModifiedBy)
                .HasMaxLength(64);
            Property(e => e.ModifiedOn);

            Property(e => e.Deleted).IsRequired();
        }
    }
}
