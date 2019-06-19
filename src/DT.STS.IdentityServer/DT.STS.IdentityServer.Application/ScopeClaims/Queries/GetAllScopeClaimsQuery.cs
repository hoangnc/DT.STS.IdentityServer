using MediatR;
using System.Collections.Generic;

namespace DT.STS.IdentityServer.Application.ScopeScopeClaims.Queries
{
    public class GetAllScopeClaimsQuery : IRequest<List<GetAllScopeClaimsDto>>
    {
    }
}
