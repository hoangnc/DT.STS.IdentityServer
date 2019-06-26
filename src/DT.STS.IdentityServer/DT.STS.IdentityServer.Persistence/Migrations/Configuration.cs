namespace DT.STS.IdentityServer.Persistence.Migrations
{
    using DT.STS.IdentityServer.Domain.Entities;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using static DT.Core.Web.Common.Identity.Constants;

    internal sealed class Configuration : DbMigrationsConfiguration<DT.STS.IdentityServer.Persistence.STSDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "DT.STS.IdentityServer.Persistence.STSDbContext";
        }

        protected override void Seed(DT.STS.IdentityServer.Persistence.STSDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            SeedScopeClaim(context);

            SeedClaims(context);
        }

        private void SeedClaims(STSDbContext context)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim
                {
                    Name = DtClaimTypes.Permission,
                    Value = DtPermissionBaseTypes.Delete,
                    CreatedOn = DateTime.Now,
                    CreatedBy = "nguyenconghoang",
                    Deleted = false
                },
                new Claim
                {
                    Name = DtClaimTypes.Permission,
                    Value = DtPermissionBaseTypes.Export,
                    CreatedOn = DateTime.Now,
                    CreatedBy = "nguyenconghoang",
                    Deleted = false
                },
                new Claim
                {
                    Name = DtClaimTypes.Permission,
                    Value = DtPermissionBaseTypes.Import,
                    CreatedOn = DateTime.Now,
                    CreatedBy = "nguyenconghoang",
                    Deleted = false
                },
                new Claim
                {
                    Name = DtClaimTypes.Permission,
                    Value = DtPermissionBaseTypes.Read,
                    CreatedOn = DateTime.Now,
                    CreatedBy = "nguyenconghoang",
                    Deleted = false
                },
                new Claim
                {
                    Name = DtClaimTypes.Permission,
                    Value = DtPermissionBaseTypes.Sync,
                    CreatedOn = DateTime.Now,
                    CreatedBy = "nguyenconghoang",
                    Deleted = false
                },
                new Claim
                {
                    Name = DtClaimTypes.Permission,
                    Value = DtPermissionBaseTypes.Update,
                    CreatedOn = DateTime.Now,
                    CreatedBy = "nguyenconghoang",
                    Deleted = false
                },
                new Claim
                {
                    Name = DtClaimTypes.Permission,
                    Value = DtPermissionBaseTypes.View,
                    CreatedOn = DateTime.Now,
                    CreatedBy = "nguyenconghoang",
                    Deleted = false
                },
                new Claim
                {
                    Name = DtClaimTypes.Permission,
                    Value = DtPermissionBaseTypes.Write,
                    CreatedOn = DateTime.Now,
                    CreatedBy = "nguyenconghoang",
                    Deleted = false
                }
            };

            context.Claims.AddOrUpdate(claim => new { claim.Name, claim.Value }, claims.ToArray());
        }

        private void SeedScopeClaim(STSDbContext context)
        {
            List<ScopeClaim> scopeClaims = new List<ScopeClaim>();

            scopeClaims.Add(new ScopeClaim
            {
                Name = "role",
                Description = "Role",
                AlwaysIncludeInIdToken = false,
                CreatedOn = DateTime.Now,
                CreatedBy = "nguyenconghoang",
                Deleted = false
            });

            scopeClaims.Add(new ScopeClaim
            {
                Name = DtClaimTypes.Permission,
                AlwaysIncludeInIdToken = false,
                CreatedBy = "nguyenconghoang",
                CreatedOn = DateTime.Now,
                Deleted = false
            });

            context.ScopeClaims.AddOrUpdate(scopeClaim => new { scopeClaim.Name }, scopeClaims.ToArray());
            context.SaveChanges();
        }
    }
}
