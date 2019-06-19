using DT.STS.IdentityServer.Application.Mapper;
using DT.STS.IdentityServer.Common.Data;
using DT.STS.IdentityServer.Common.Models;
using DT.STS.IdentityServer.Domain.Entities;
using DT.STS.IdentityServer.Persistence;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DT.STS.IdentityServer.Application.Users.Queries.SearchUsersByToken
{
    public class SearchUsersByTokenPagedQueryHandler : IRequestHandler<SearchUsersByTokenPagedQuery, DataSourceResult>
    {
        private readonly STSDbContext _context;
        public SearchUsersByTokenPagedQueryHandler(STSDbContext context)
        {
            _context = context;
        }

        public async Task<DataSourceResult> Handle(SearchUsersByTokenPagedQuery request, CancellationToken cancellationToken)
        {
            IQueryable<User> query = _context.Users.AsQueryable();
            query = query.Where(u => !u.Deleted);
            if (!string.IsNullOrWhiteSpace(request.Token))
            {
                query = query.Where(u => u.UserName.Contains(request.Token)
                || u.LastName.Contains(request.Token)
                || u.FirstName.Contains(request.Token)
                || u.Email.Contains(request.Token)
                || u.DepartmentName.Contains(request.Token)
                || u.ManagerName.Contains(request.Token));
            }
            query = query.OrderByDescending(u => u.CreatedOn);

            PagedList<User> queryResult = new PagedList<User>();
            await queryResult.CreateAsync(query, request.DataSourceRequest.PageNum, request.DataSourceRequest.PageSize);
            List<SearchUsersByTokenPagedDto> data = queryResult.Select(u => u.ToSearchUsersByTokenDto()).ToList();

            return new DataSourceResult
            {
                Data = data,
                Total = queryResult.TotalCount
            };
        }
    }
}
