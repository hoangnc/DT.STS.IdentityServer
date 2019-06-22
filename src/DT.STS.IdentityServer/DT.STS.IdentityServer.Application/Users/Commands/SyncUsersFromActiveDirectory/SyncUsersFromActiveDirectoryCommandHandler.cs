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
using Wdc.DirectoryLib.Types;

namespace DT.STS.IdentityServer.Application.Users.Commands
{
    public class SyncUsersFromActiveDirectoryCommandHandler : IRequestHandler<SyncUsersFromActiveDirectoryCommand, int>
    {
        private readonly STSDbContext _context;
        private string Domain => ConfigurationManager.AppSettings["domain"];

        public SyncUsersFromActiveDirectoryCommandHandler(STSDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(SyncUsersFromActiveDirectoryCommand request, CancellationToken cancellationToken)
        {
            using (DirectoryEntry entry = new DirectoryEntry("LDAP://" + Domain))
            using (DirectorySearcher search = new DirectorySearcher(entry))
            {
                search.PageSize = 5000;
                search.Filter = "(&(objectClass=user)(objectCategory=person))";
                SearchResult result;
                SearchResultCollection resultCol = search.FindAll();
                List<User> users = new List<User>();
                if (resultCol != null)
                {
                    for (int i = 0; i < resultCol.Count; i++)
                    {
                        result = resultCol[i];
                        UserAccount userAccount = ActiveDirectoryHelper.GetUserFromResult(result);
                        if (userAccount != null)
                        {
                            if (!string.IsNullOrWhiteSpace(userAccount.Department)
                                && !string.IsNullOrWhiteSpace(userAccount.SamAccountName))
                            {
                                string manager = string.Empty;
                                if (!string.IsNullOrEmpty(userAccount.Manager))
                                {
                                    DirectoryEntry managerEntry = new DirectoryEntry($"LDAP://{Domain}/{userAccount.Manager}");
                                    manager = managerEntry.Properties["samAccountName"][0].ToString();
                                }

                                List<string> directReports = new List<string>();
                                if (userAccount.DirectReports.Any())
                                {
                                    foreach (string directReport in directReports.Where(d => !string.IsNullOrEmpty(d)))
                                    {
                                        DirectoryEntry managerEntry = new DirectoryEntry($"LDAP://{Domain}/{directReport}");
                                        directReports.Add(managerEntry.Properties["samAccountName"][0].ToString());
                                    }
                                }

                                users.Add(new User
                                {
                                    UserName = userAccount.SamAccountName,
                                    Active = true,
                                    CreatedBy = request.CreatedBy,
                                    CreatedOn = request.CreatedOn,
                                    DepartmentName = userAccount.Department,
                                    DirectReports = string.Join(";", directReports),
                                    FirstName = userAccount.GivenName,
                                    LastName = userAccount.SurName,
                                    Email = userAccount.UserPrincipalName,
                                    JpegPhoto = userAccount.JpegPhoto,
                                    ManagerName = manager
                                });
                            }
                        }
                    }
                }
                _context.Users.AddOrUpdate(user => new
                {
                    user.UserName,
                    user.DepartmentName
                }, users.ToArray());
                return await _context.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
