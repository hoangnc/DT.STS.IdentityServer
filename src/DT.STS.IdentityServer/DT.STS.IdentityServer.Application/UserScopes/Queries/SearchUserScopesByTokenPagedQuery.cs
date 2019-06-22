using DT.STS.IdentityServer.Common.Models;
using MediatR;
using System.Collections.Generic;

namespace DT.STS.IdentityServer.Application.UserScopes.Queries
{
    public class SearchUserScopesByTokenPagedQuery : IRequest<DataSourceResult>
    {
        public DataSourceRequest DataSourceRequest { get; set; }
        public string Token { get; set; }
    }
}
