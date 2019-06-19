using DT.STS.IdentityServer.Common.Data;
using DT.STS.IdentityServer.Common.Models;
using MediatR;
using System.Collections.Generic;

namespace DT.STS.IdentityServer.Application.Users.Queries
{
    public class GetUsersPagedQuery : IRequest<DataSourceResult>
    {
        public DataSourceRequest DataSourceRequest { get; set; }
    }
}
