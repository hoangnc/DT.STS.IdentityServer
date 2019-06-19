using DT.STS.IdentityServer.Common.Data;
using DT.STS.IdentityServer.Common.Models;
using DT.STS.IdentityServer.Domain.Entities;
using DT.STS.IdentityServer.Persistence;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DT.STS.IdentityServer.Application.Scopes.Queries
{
    public class SearchScopesByTokenPagedQueryHandler : IRequestHandler<SearchScopesByTokenPagedQuery, DataSourceResult>
    {
        private readonly STSDbContext _context;
        public SearchScopesByTokenPagedQueryHandler(STSDbContext context)
        {
            _context = context;
        }

        public async Task<DataSourceResult> Handle(SearchScopesByTokenPagedQuery request, CancellationToken cancellationToken)
        {
            IQueryable<Scope> query = _context.Scopes.AsQueryable();
            query = query.Where(c => !c.Deleted);
            if (!string.IsNullOrWhiteSpace(request.Token))
            {
                query = query.Where(c => c.Name.Contains(request.Token)
                || c.DisplayName.Contains(request.Token)
                || c.Description.Contains(request.Token));
            }
            query = query.OrderByDescending(u => u.CreatedOn);

            IQueryable<SearchScopesByTokenPagedDto> searchQuery = query.Select(scope => new SearchScopesByTokenPagedDto
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
                              sc=>sc.ScopeClaimId,
                              c=>c.Id, (sc, c) => c).ToList()
            });

            PagedList<SearchScopesByTokenPagedDto> queryResult = new PagedList<SearchScopesByTokenPagedDto>();
            await queryResult.CreateAsync(searchQuery, request.DataSourceRequest.PageNum, request.DataSourceRequest.PageSize);
            List<SearchScopesByTokenPagedDto> data = queryResult.ToList();

            return new DataSourceResult
            {
                Data = data,
                Total = queryResult.TotalCount
            };
        }
    }
}
