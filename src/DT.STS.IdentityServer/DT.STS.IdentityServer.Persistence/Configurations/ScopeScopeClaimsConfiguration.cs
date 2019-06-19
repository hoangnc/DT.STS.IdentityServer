using DT.STS.IdentityServer.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DT.STS.IdentityServer.Persistence.Configurations
{
    public class ScopeScopeClaimsConfiguration : EntityTypeConfiguration<ScopeScopeClaim>
    {
        public ScopeScopeClaimsConfiguration()
        {
            HasKey(e => e.Id);

            Property(e => e.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            ToTable("ScopeScopeClaim");

            Property(e => e.ScopeClaimId).IsRequired();
            Property(e => e.ScopeId).IsRequired();

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
