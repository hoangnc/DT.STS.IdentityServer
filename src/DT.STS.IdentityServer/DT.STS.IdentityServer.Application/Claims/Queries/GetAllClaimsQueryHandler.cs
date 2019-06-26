using DT.STS.IdentityServer.Application.Mapper;
using DT.STS.IdentityServer.Persistence;
using MediatR;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DT.STS.IdentityServer.Application.Claims.Queries
{
    public class GetAllClaimsQueryHandler : IRequestHandler<GetAllClaimsQuery, List<GetAllClaimsDto>>
    {
        private readonly STSDbContext _context;
        public GetAllClaimsQueryHandler(STSDbContext context)
        {
            _context = context;
        }

        public async Task<List<GetAllClaimsDto>> Handle(GetAllClaimsQuery request, CancellationToken cancellationToken)
        {
            List<Domain.Entities.Claim> claims = await _context.Claims.Where(c => !c.Deleted)
                .ToListAsync();

            return claims.Select(c => c.ToGetAllClaimsDto()).ToList();
        }
    }
}
