using DT.STS.IdentityServer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DT.STS.IdentityServer.Persistence.Configurations
{
    public class UserScopeConfiguration : EntityTypeConfiguration<UserScope>
    {
        public UserScopeConfiguration()
        {
            HasKey(e => new { e.Id, e.ScopeName });

            Property(e => e.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            ToTable("UserScope");

            Property(e => e.Users);
            Property(e => e.ScopeName);

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
