using DT.STS.IdentityServer.Persistence;
using MediatR;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
namespace DT.STS.IdentityServer.Application.Scopes.Queries
{
    public class GetScopeByIdQueryHandler : IRequestHandler<GetScopeByIdQuery, GetScopeByIdDto>
    {
        private readonly STSDbContext _context;
        public GetScopeByIdQueryHandler(STSDbContext context)
        {
            _context = context;
        }

        public async Task<GetScopeByIdDto> Handle(GetScopeByIdQuery request, CancellationToken cancellationToken)
        {
            return await _context.Scopes.Where(s => s.Id == request.Id)
                .Select(scope => new GetScopeByIdDto
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
                }).FirstOrDefaultAsync();
        }
    }
}
