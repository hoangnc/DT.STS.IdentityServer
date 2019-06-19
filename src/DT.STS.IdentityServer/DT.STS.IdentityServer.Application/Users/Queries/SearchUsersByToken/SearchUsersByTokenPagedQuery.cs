using DT.STS.IdentityServer.Common.Models;
using MediatR;

namespace DT.STS.IdentityServer.Application.Users.Queries
{
    public class SearchUsersByTokenPagedQuery : IRequest<DataSourceResult>
    {
        public DataSourceRequest DataSourceRequest { get; set; }
        public string Token { get; set; }
    }
}
