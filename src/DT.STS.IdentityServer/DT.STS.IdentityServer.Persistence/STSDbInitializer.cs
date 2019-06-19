using DT.STS.IdentityServer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace DT.STS.IdentityServer.Persistence
{
    public class STSDbInitializer : CreateDatabaseIfNotExists<STSDbContext>
    {
        protected override void Seed(STSDbContext context)
        {
            SeedScopeClaims(context);
            base.Seed(context);
        }

        private void SeedScopeClaims(STSDbContext context)
        {
            List<ScopeClaim> scopeClaims = new List<ScopeClaim>();

            scopeClaims.Add(new ScopeClaim
            {
                Name = "read",
                Description = "Read data",
                CreatedOn = DateTime.Now,
                CreatedBy = "nguyenconghoang",
                Deleted = false,
                AlwaysIncludeInIdToken = false,
            });

            scopeClaims.Add(new ScopeClaim
            {
                Name = "write",
                Description = "Write data",
                AlwaysIncludeInIdToken = false,
                CreatedOn = DateTime.Now,
                CreatedBy = "nguyenconghoang",
                Deleted = false
            });

            scopeClaims.Add(new ScopeClaim
            {
                Name = "modify",
                Description = "Modify data",
                AlwaysIncludeInIdToken = false,
                CreatedOn = DateTime.Now,
                CreatedBy = "nguyenconghoang",
                Deleted = false
            });

            scopeClaims.Add(new ScopeClaim
            {
                Name = "delete",
                Description = "Delete data",
                AlwaysIncludeInIdToken = false,
                CreatedOn = DateTime.Now,
                CreatedBy = "nguyenconghoang",
                Deleted = false
            });

            context.ScopeClaims.AddRange(scopeClaims);
            context.SaveChanges();
        }
    }
}
