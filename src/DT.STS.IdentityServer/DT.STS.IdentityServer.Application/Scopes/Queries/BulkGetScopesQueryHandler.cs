using DT.STS.IdentityServer.Persistence;
using MediatR;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DT.STS.IdentityServer.Application.Scopes.Queries
{
    public class BulkGetScopesQueryHandler : IRequestHandler<BulkGetScopesQuery, List<BulkGetScopesDto>>
    {
        private readonly STSDbContext _context;
        public BulkGetScopesQueryHandler(STSDbContext context)
        {
            _context = context;
        }

        public async Task<List<BulkGetScopesDto>> Handle(BulkGetScopesQuery request, CancellationToken cancellationToken)
        {
            return await _context.Scopes
                .Where(scope => request.ScopeNames.Contains(scope.Name))
                .Select(scope => new BulkGetScopesDto
                {
                    Id = scope.Id,
                    ClaimsRule = scope.ClaimsRule,
                    Description = scope.Description,
                    DisplayName = scope.DisplayName,
                    Type = scope.Type,
                    Emphasize = scope.Emphasize,
                    Enabled = scope.Enabled,
                    IncludeAllClaimsForUser = scope.IncludeAllClaimsForUser,
                    ShowInDiscoveryDocument = scope.ShowInDiscoveryDocument,
                    Name = scope.Name,
                    Required = scope.Required,
                    ScopeClaims = _context.ScopeScopeClaims.Where(sc => sc.ScopeId == scope.Id)
                              .Join(_context.ScopeClaims,
                              sc => sc.ScopeClaimId,
                              c => c.Id, (sc, c) => c).ToList()
                }).ToListAsync();
        }
    }
}
