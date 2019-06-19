using DT.STS.IdentityServer.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DT.STS.IdentityServer.Persistence.Configurations
{
    public class UserConfiguration : EntityTypeConfiguration<User>
    {
        public UserConfiguration()
        {
            HasKey(e => e.Id);

            Property(e => e.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            ToTable("User");

            Property(e => e.UserName).IsRequired()
                .HasMaxLength(64);

            Property(e => e.Password)
                .HasMaxLength(256);

            Property(e => e.FirstName)
               .HasMaxLength(64);

            Property(e => e.LastName)
               .HasMaxLength(64);

            Property(e => e.FullNameUnicode)
               .HasMaxLength(64);

            Property(e => e.ManagerName)
               .HasMaxLength(64);

            Property(e => e.DepartmentName);

            Property(e => e.Email)
                .HasMaxLength(64);

            Property(e => e.Domain)
                .HasMaxLength(64);

            Property(e => e.JpegPhoto);

            Property(e => e.DirectReports);

            Property(e => e.Active);

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
