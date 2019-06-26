using DT.STS.IdentityServer.Common.Data;
using DT.STS.IdentityServer.Common.Models;
using DT.STS.IdentityServer.Domain.Entities;
using DT.STS.IdentityServer.Persistence;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DT.STS.IdentityServer.Application.UserScopes.Queries
{
    public class SearchUserScopesByTokenPagedQueryHandler : IRequestHandler<SearchUserScopesByTokenPagedQuery, DataSourceResult>
    {
        private readonly STSDbContext _context;
        public SearchUserScopesByTokenPagedQueryHandler(STSDbContext context)
        {
            _context = context;
        }

        public async Task<DataSourceResult> Handle(SearchUserScopesByTokenPagedQuery request, CancellationToken cancellationToken)
        {
            IQueryable<Scope> query = _context.Scopes.AsQueryable();
            query = query.Where(u => !u.Deleted);

            IQueryable<SearchUserScopesByTokenPagedDto> userScopesQuery = query.SelectMany(s => _context.UserScopes
                .Where(us => us.ScopeName == s.Name)
                .DefaultIfEmpty()
                .Select(us => new SearchUserScopesByTokenPagedDto
                {
                    ScopeName = s.Name,
                    Users = us.Users,
                    Permissions = us.Permissions              
                }));

            if (!string.IsNullOrWhiteSpace(request.Token))
            {
                userScopesQuery = userScopesQuery.Where(u => u.Users.Contains(request.Token)
                || u.ScopeName.Contains(request.Token));
            }
            userScopesQuery = userScopesQuery.OrderBy(u => u.ScopeName);

            PagedList<SearchUserScopesByTokenPagedDto> queryResult = new PagedList<SearchUserScopesByTokenPagedDto>();
            await queryResult.CreateAsync(userScopesQuery, request.DataSourceRequest.PageNum, request.DataSourceRequest.PageSize);
            /*List<SearchUserScopesByTokenPagedDto> data = queryResult.Select(u => new SearchUserScopesByTokenPagedDto
            {
                ScopeName = u.ScopeName,
                Users = u.Users,
                Permissions = u.Permissions
            }).ToList();*/

            return new DataSourceResult
            {
                Data = queryResult,
                Total = queryResult.TotalCount
            };
        }
    }
}
