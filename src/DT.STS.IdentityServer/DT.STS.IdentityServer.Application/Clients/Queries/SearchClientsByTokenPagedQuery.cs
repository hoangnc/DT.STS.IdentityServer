using DT.STS.IdentityServer.Common.Models;
using MediatR;

namespace DT.STS.IdentityServer.Application.Clients.Queries
{
    public class SearchClientsByTokenPagedQuery : IRequest<DataSourceResult>
    {
        public DataSourceRequest DataSourceRequest { get; set; }
        public string Token { get; set; }
    }
}
