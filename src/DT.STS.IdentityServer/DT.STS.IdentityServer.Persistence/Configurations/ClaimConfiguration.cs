using DT.STS.IdentityServer.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DT.STS.IdentityServer.Persistence.Configurations
{
    public class ClaimConfiguration : EntityTypeConfiguration<Claim>
    {
        public ClaimConfiguration()
        {
            HasKey(e => new { e.Id, e.Name, e.Value });

            Property(e => e.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            ToTable("Claim");

            Property(e => e.Name).IsRequired()
                .HasMaxLength(128);

            Property(e => e.Value).IsRequired()
                .HasMaxLength(128);

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
