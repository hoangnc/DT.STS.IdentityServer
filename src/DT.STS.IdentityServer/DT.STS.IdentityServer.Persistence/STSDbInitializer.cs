using DT.STS.IdentityServer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using static DT.Core.Web.Common.Identity.Constants;

namespace DT.STS.IdentityServer.Persistence
{
    public class STSDbInitializer : CreateDatabaseIfNotExists<STSDbContext>
    {
        private const string UserName = "NguyenCongHoang";
        protected override void Seed(STSDbContext context)
        {
            base.Seed(context);
            SeedClaims(context);
            SeedScopeClaims(context);
            SeedCompany(context);
            SeedUserScope(context);
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

        private void SeedCompany(STSDbContext context)
        {
            List<Company> companies = new List<Company>();

            companies.Add(new Company
            {
                Code = "TanLoiPhat",
                Name = "Tân Lợi Phát",
                ShortName = "Tân Lợi Phát",
                CreatedBy = "nguyenconghoang",
                CreatedOn = DateTime.Now,
                Deleted = false
            });

            companies.Add(new Company
            {
                Code = "DuyTan",
                Name = "Duy Tân",
                ShortName = "Duy Tân",
                CreatedBy = "nguyenconghoang",
                CreatedOn = DateTime.Now,
                Deleted = false
            });

            companies.Add(new Company
            {
                Code = "BinhDuong",
                Name = "Bình Dương",
                ShortName = "Bình Dương",
                CreatedBy = "nguyenconghoang",
                CreatedOn = DateTime.Now,
                Deleted = false
            });

            companies.Add(new Company
            {
                Code = "TranDaiNghia",
                Name = "Trần Đại Nghĩa",
                ShortName = "Trần Đại Nghĩa",
                CreatedBy = "nguyenconghoang",
                CreatedOn = DateTime.Now,
                Deleted = false
            });

            companies.Add(new Company
            {
                Code = "DuyPhong",
                Name = "Duy Phong",
                ShortName = "Duy Phong",
                CreatedBy = "nguyenconghoang",
                CreatedOn = DateTime.Now,
                Deleted = false
            });

            companies.Add(new Company
            {
                Code = "Natec",
                Name = "Natec",
                ShortName = "Natec",
                CreatedBy = "nguyenconghoang",
                CreatedOn = DateTime.Now,
                Deleted = false
            });

            companies.Add(new Company
            {
                Code = "Plascene",
                Name = "Plascene",
                ShortName = "Plascene",
                CreatedBy = "nguyenconghoang",
                CreatedOn = DateTime.Now,
                Deleted = false
            });

            companies.Add(new Company
            {
                Code = "DucHoa",
                Name = "Đức Hòa",
                ShortName = "Đức Hòa",
                CreatedBy = "nguyenconghoang",
                CreatedOn = DateTime.Now,
                Deleted = false
            });

            companies.Add(new Company
            {
                Code = "Mata",
                Name = "Mata",
                ShortName = "Mata",
                CreatedBy = "nguyenconghoang",
                CreatedOn = DateTime.Now,
                Deleted = false
            });

            companies.Add(new Company
            {
                Code = "BaCayCau",
                Name = "Ba Cây Cau",
                ShortName = "Ba Cây Cau",
                CreatedBy = "nguyenconghoang",
                CreatedOn = DateTime.Now,
                Deleted = false
            });

            companies.Add(new Company
            {
                Code = "DongXanh",
                Name = "Đồng Xanh",
                ShortName = "Đồng Xanh",
                CreatedBy = "nguyenconghoang",
                CreatedOn = DateTime.Now,
                Deleted = false
            });

            companies.Add(new Company
            {
                Code = "CatVan",
                Name = "Cát Vân",
                ShortName = "Cát Vân",
                CreatedBy = "nguyenconghoang",
                CreatedOn = DateTime.Now,
                Deleted = false
            });

            companies.Add(new Company
            {
                Code = "Mida",
                Name = "Mida",
                ShortName = "Mida",
                CreatedBy = "nguyenconghoang",
                CreatedOn = DateTime.Now,
                Deleted = false
            });

            companies.Add(new Company
            {
                Code = "QuangPhuoc",
                Name = "Quang Phước",
                ShortName = "Quang Phước",
                CreatedBy = "nguyenconghoang",
                CreatedOn = DateTime.Now,
                Deleted = false
            });

            companies.Add(new Company
            {
                Code = "DaNang",
                Name = "Đà Nẵng",
                ShortName = "Đà Nẵng",
                CreatedBy = "nguyenconghoang",
                CreatedOn = DateTime.Now,
                Deleted = false
            });

            companies.Add(new Company
            {
                Code = "Deloitte",
                Name = "Deloitte",
                ShortName = "Deloitte",
                CreatedBy = "nguyenconghoang",
                CreatedOn = DateTime.Now,
                Deleted = false
            });

            companies.Add(new Company
            {
                Code = "DongLam",
                Name = "Đồng Lâm",
                ShortName = "Đồng Lâm",
                CreatedBy = "nguyenconghoang",
                CreatedOn = DateTime.Now,
                Deleted = false
            });
            context.Companies.AddOrUpdate(company => new { company.Code }, companies.ToArray());
            context.SaveChanges();
        }

        private void SeedUserScope(STSDbContext context)
        {
            List<UserScope> userScopes = new List<UserScope>();

            userScopes.Add(new UserScope {
                ScopeName="adminapi",
                Permissions = "permission_delete;permission_export;permission_import;permission_read;permission_update;permission_write",
                Users = UserName,
                Deleted = false,
                CreatedOn = DateTime.Now,
                CreatedBy = UserName
            });

            userScopes.Add(new UserScope
            {
                ScopeName = "adminmvc",
                Permissions = "permission_delete;permission_export;permission_import;permission_read;permission_update;permission_write",
                Users = UserName,
                Deleted = false,
                CreatedOn = DateTime.Now,
                CreatedBy = UserName
            });

            context.UserScopes.AddOrUpdate(userscope => new { userscope.ScopeName }, userScopes.ToArray());
            context.SaveChanges();
        }
    }
}
