using DT.STS.IdentityServer.Common.Helpers;
using DT.STS.IdentityServer.Domain.Entities;
using DT.STS.IdentityServer.Persistence;
using MediatR;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Migrations;
using System.DirectoryServices;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
namespace DT.STS.IdentityServer.Application.Departments.Commands
{
    public class SyncDepartmentsFromActiveDirectoryCommandHandler : IRequestHandler<SyncDepartmentsFromActiveDirectoryCommand, int>
    {
        private readonly STSDbContext _context;
        private string Domain => ConfigurationManager.AppSettings["domain"];

        public SyncDepartmentsFromActiveDirectoryCommandHandler(STSDbContext context)
        {
            _context = context;
        }
        
        public async Task<int> Handle(SyncDepartmentsFromActiveDirectoryCommand request, CancellationToken cancellationToken)
        {
            using (DirectoryEntry entry = new DirectoryEntry("LDAP://" + Domain))
            using (DirectorySearcher search = new DirectorySearcher(entry))
            {
                search.PageSize = 5000;
                search.Filter = "(&(objectClass=user)(objectCategory=person))";
                SearchResult result;
                SearchResultCollection resultCol = search.FindAll();
                List<Department> departments = new List<Department>();
                if (resultCol != null)
                {
                    for (int i = 0; i < resultCol.Count; i++)
                    {
                        result = resultCol[i];
                        
                        var userAccountControl = ActiveDirectoryHelper.TryGetResult<int>(result, "userAccountControl");
                        var active = userAccountControl == 512 ? true : false;
                        if (active)
                        {
                            string departmentName = ActiveDirectoryHelper.TryGetResult<string>(result, "department");
                            if (!string.IsNullOrEmpty(departmentName))
                            {
                                if (!departments.Any(d => d.Name == departmentName))
                                {
                                    departments.Add(new Department
                                    {
                                        Code = departmentName,
                                        Name = departmentName,
                                        CreatedBy = request.CreatedBy,
                                        CreatedOn = request.CreatedOn
                                    });
                                }
                            }
                        }
                    }
                }
                _context.Departments.AddOrUpdate(department => new
                {
                    department.Code,
                    department.Name
                }, departments.ToArray());
                return await _context.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
