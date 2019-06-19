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

namespace DT.STS.IdentityServer.Application.Users.Queries
{
    public class GetUsersPagedQueryHandler : IRequestHandler<GetUsersPagedQuery, DataSourceResult>
    {
        private readonly STSDbContext _context;
        public GetUsersPagedQueryHandler(STSDbContext context)
        {
            _context = context;
        }

        public async Task<DataSourceResult> Handle(GetUsersPagedQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Users.AsQueryable();
            query = query.Where(u => !u.Deleted);
            query = query.OrderBy(u => u.CreatedOn);

            PagedList<User> queryResult = new PagedList<User>();
            await queryResult.CreateAsync(query, request.DataSourceRequest.PageNum, request.DataSourceRequest.PageSize);
            List<GetUsersPagedDto> data = queryResult.Select(u => u.ToGetUsersPagedDto()).ToList();

            return new DataSourceResult
            {
                Data = data,
                Total = queryResult.TotalCount
            };
        }
    }
}
