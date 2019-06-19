using DT.STS.IdentityServer.Domain.Entities;
using DT.STS.IdentityServer.Persistence.Configurations;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DT.STS.IdentityServer.Persistence
{
    public class STSDbContext : DbContext
    {
        public STSDbContext() : base(ConfigurationManager.ConnectionStrings["STSConnectionString"].ConnectionString)
        {
            Database.SetInitializer(new STSDbInitializer());
        }

        public STSDbContext(string stsConnectionString) : base(stsConnectionString)
        {
            Database.SetInitializer(new STSDbInitializer());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Adds configurations for entities from separate class
            modelBuilder.Configurations.Add(new UserConfiguration());
            modelBuilder.Configurations.Add(new DepartmentConfiguration());
            modelBuilder.Configurations.Add(new RoleConfiguration());
            modelBuilder.Configurations.Add(new ScopeConfiguration());
            modelBuilder.Configurations.Add(new ScopeClaimConfiguration());
            modelBuilder.Configurations.Add(new ScopeScopeClaimsConfiguration());
            modelBuilder.Configurations.Add(new ClientConfiguration());
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Scope> Scopes { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<ScopeClaim> ScopeClaims { get; set; }
        public DbSet<ScopeScopeClaim> ScopeScopeClaims { get; set; }
    }
}
