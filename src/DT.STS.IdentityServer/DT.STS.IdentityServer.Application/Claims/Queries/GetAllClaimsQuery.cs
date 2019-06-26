using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DT.STS.IdentityServer.Application.Claims.Queries
{
    public class GetAllClaimsQuery : IRequest<List<GetAllClaimsDto>>
    {
    }
}
