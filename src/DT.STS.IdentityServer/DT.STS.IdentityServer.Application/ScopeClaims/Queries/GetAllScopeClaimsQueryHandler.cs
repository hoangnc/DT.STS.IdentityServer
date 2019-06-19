using DT.STS.IdentityServer.Application.Mapper;
using DT.STS.IdentityServer.Domain.Entities;
using DT.STS.IdentityServer.Persistence;
using MediatR;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DT.STS.IdentityServer.Application.ScopeScopeClaims.Queries
{
    public class GetAllScopeClaimsQueryHandler : IRequestHandler<GetAllScopeClaimsQuery, List<GetAllScopeClaimsDto>>
    {
        private readonly STSDbContext _context;

        public GetAllScopeClaimsQueryHandler(STSDbContext context)
        {
            _context = context;
        }

        public async Task<List<GetAllScopeClaimsDto>> Handle(GetAllScopeClaimsQuery request, CancellationToken cancellationToken)
        {
            List<ScopeClaim> scopeClaims = await _context.ScopeClaims.ToListAsync();

            List<GetAllScopeClaimsDto> result = scopeClaims.Select(scopeClaim => scopeClaim.ToGetAllScopeClaimsDto()).ToList();
            return result;
        }
    }
}
