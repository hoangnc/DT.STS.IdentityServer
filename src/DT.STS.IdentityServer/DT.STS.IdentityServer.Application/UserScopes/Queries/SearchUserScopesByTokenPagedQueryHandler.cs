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
            IQueryable<UserScope> query = _context.UserScopes.AsQueryable();
            query = query.Where(u => !u.Deleted);

            if (!string.IsNullOrWhiteSpace(request.Token))
            {
                query = query.Where(u => u.Users.Contains(request.Token)
                || u.ScopeName.Contains(request.Token));
            }
            query = query.OrderByDescending(u => u.CreatedOn);

            PagedList<UserScope> queryResult = new PagedList<UserScope>();
            await queryResult.CreateAsync(query, request.DataSourceRequest.PageNum, request.DataSourceRequest.PageSize);
            List<SearchUserScopesByTokenPagedDto> data = queryResult.Select(u => new SearchUserScopesByTokenPagedDto
            {
                ScopeName = u.ScopeName,
                Users = u.Users
            }).ToList();

            return new DataSourceResult
            {
                Data = data,
                Total = queryResult.TotalCount
            };
        }
    }
}
