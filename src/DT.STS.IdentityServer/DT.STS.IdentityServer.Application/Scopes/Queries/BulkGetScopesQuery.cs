using MediatR;
using System.Collections.Generic;

namespace DT.STS.IdentityServer.Application.Scopes.Queries
{
    public class BulkGetScopesQuery : IRequest<List<BulkGetScopesDto>>
    {
        public IEnumerable<string> ScopeNames { get; set; }
    }
}
