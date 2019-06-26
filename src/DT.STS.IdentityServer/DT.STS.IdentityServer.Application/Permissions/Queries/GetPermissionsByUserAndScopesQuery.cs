using MediatR;
using System.Collections.Generic;

namespace DT.STS.IdentityServer.Application.Permissions.Queries
{
    public class GetPermissionsByUserAndScopesQuery : IRequest<List<GetPermissionsByUserAndScopesDto>>
    {
        public string UserName { get; set; }
        public List<string> Scopes { get; set; }
    }
}
