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
    public class ClientConfiguration : EntityTypeConfiguration<Client>
    {
        public ClientConfiguration()
        {
            HasKey(e => new { e.Id, e.ClientId });

            Property(e => e.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            ToTable("Client");

            Property(e => e.ClientId).IsRequired()
                .HasMaxLength(128);

            Property(e => e.ClientName).IsRequired()
                .HasMaxLength(256);

            Property(e => e.ClientUri).IsRequired()
                .HasMaxLength(256);

            Property(e => e.EnableLocalLogin);
            Property(e => e.Flow);
            Property(e => e.IdentityTokenLifetime);
            Property(e => e.IncludeJwtId);
            Property(e => e.LogoUri).HasMaxLength(256);
            Property(e => e.LogoutSessionRequired);
            Property(e => e.LogoutUri).HasMaxLength(256);
            Property(e => e.PostLogoutRedirectUris).HasMaxLength(4000);
            Property(e => e.PrefixClientClaims);
            Property(e => e.RedirectUris).HasMaxLength(4000);
            Property(e => e.RefreshTokenExpiration);
            Property(e => e.RefreshTokenUsage);
            Property(e => e.RequireConsent);
            Property(e => e.RequireSignOutPrompt);
            Property(e => e.SlidingRefreshTokenLifetime);
            Property(e => e.UpdateAccessTokenClaimsOnRefresh);

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
