using MediatR;
using System.Collections.Generic;

namespace DT.STS.IdentityServer.Application.Scopes.Queries
{
    public class GetScopesQuery : IRequest<List<GetScopesDto>>
    {
    }
}
