namespace DT.STS.IdentityServer.Persistence.Migrations
{
    using DT.STS.IdentityServer.Domain.Entities;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;

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

            context.ScopeClaims.AddOrUpdate(scopeClaim => new { scopeClaim.Name }, scopeClaims.ToArray());
            context.SaveChanges();
        }
    }
}
