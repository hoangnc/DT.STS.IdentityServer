using DT.STS.IdentityServer.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DT.STS.IdentityServer.Persistence.Configurations
{
    public class CompanyConfiguration : EntityTypeConfiguration<Company>
    {
        public CompanyConfiguration()
        {
            HasKey(e => e.Id);

            Property(e => e.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            ToTable("Company");

            Property(e => e.Code).IsRequired()
                       .HasMaxLength(64);

            Property(e => e.Name).IsRequired()
                .HasMaxLength(128);

            Property(e => e.ShortName)
                        .HasMaxLength(64);

            Property(e => e.Description)
           .HasMaxLength(1000);

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
