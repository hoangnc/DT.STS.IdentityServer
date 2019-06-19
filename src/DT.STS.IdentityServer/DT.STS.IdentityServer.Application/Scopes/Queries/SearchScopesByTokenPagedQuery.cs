using DT.STS.IdentityServer.Common.Models;
using MediatR;

namespace DT.STS.IdentityServer.Application.Scopes.Queries
{
    public class SearchScopesByTokenPagedQuery : IRequest<DataSourceResult>
    {
        public DataSourceRequest DataSourceRequest { get; set; }
        public string Token { get; set; }
    }
}
