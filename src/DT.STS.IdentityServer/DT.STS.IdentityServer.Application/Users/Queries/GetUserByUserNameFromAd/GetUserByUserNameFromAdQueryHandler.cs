using DT.STS.IdentityServer.Common.Helpers;
using DT.STS.IdentityServer.Persistence;
using MediatR;
using System.Collections.Generic;
using System.Configuration;
using System.DirectoryServices;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Wdc.DirectoryLib.Types;

namespace DT.STS.IdentityServer.Application.Users.Queries
{
    public class GetUserByUserNameFromAdQueryHandler : IRequestHandler<GetUserByUserNameFromAdQuery, GetUserByUserNameFromAdDto>
    {
        private readonly STSDbContext _context;
        private string Domain => ConfigurationManager.AppSettings["domain"];
        public GetUserByUserNameFromAdQueryHandler(STSDbContext context)
        {
            _context = context;
        }

        public Task<GetUserByUserNameFromAdDto> Handle(GetUserByUserNameFromAdQuery request, CancellationToken cancellationToken)
        {
            GetUserByUserNameFromAdDto getUserByUserNameFromAdDto = null;

            if (!string.IsNullOrWhiteSpace(request.UserName))
            {
                using (DirectoryEntry entry = new DirectoryEntry("LDAP://" + Domain))
                using (DirectorySearcher search = new DirectorySearcher(entry))
                {
                    search.PageSize = 5000;
                    search.Filter = $"(&(objectClass=user)(|(CN=something)(sAMAccountName={request.UserName})))";

                    SearchResult result = search.FindOne();

                    if (result != null)
                    {
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

                                getUserByUserNameFromAdDto = new GetUserByUserNameFromAdDto
                                {
                                    UserName = userAccount.SamAccountName,
                                    DepartmentName = userAccount.Department,
                                    DirectReports = string.Join(";", directReports),
                                    FirstName = userAccount.GivenName,
                                    LastName = userAccount.SurName,
                                    ManagerName = manager,
                                    Email = userAccount.Email
                                };

                                return Task.FromResult(getUserByUserNameFromAdDto);
                            }
                        }
                    }
                   
                }
            }
            return Task.FromResult(getUserByUserNameFromAdDto);
        }
    }
}
