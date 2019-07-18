using DT.Core.Text;
using MediatR;
using System.Collections.Generic;
using System.Configuration;
using System.DirectoryServices;
using System.Threading;
using System.Threading.Tasks;

namespace DT.STS.IdentityServer.Application.ActiveDirectories.Queries
{
    public class GetAllGroupsInActiveDirectoryQueryHandler : IRequestHandler<GetAllGroupsInActiveDirectoryQuery, List<string>>
    {
        private string Domain => ConfigurationManager.AppSettings["domain"];

        public Task<List<string>> Handle(GetAllGroupsInActiveDirectoryQuery request, CancellationToken cancellationToken)
        {
            using (DirectoryEntry entry = new DirectoryEntry("LDAP://" + Domain))
            using (DirectorySearcher search = new DirectorySearcher(entry))
            {
                List<string> groups = new List<string>();
                search.PageSize = 5000;
                search.Filter = "(&(objectCategory=group)(groupType:1.2.840.113556.1.4.803:=8))";
                search.SearchScope = SearchScope.Subtree;
                SearchResultCollection searchResults = search.FindAll();
                foreach (SearchResult searchResult in searchResults)
                {
                    string group = searchResult.Properties["cn"][0].ToString();
                    if (!group.IsNullOrEmpty())
                    {
                        groups.Add(group);
                    }
                }
                return Task.FromResult(groups);
            }
        }
    }
}
