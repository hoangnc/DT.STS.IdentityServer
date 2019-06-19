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

namespace DT.STS.IdentityServer.Application.Departments.Queries
{
    public class SearchDepartmentsByTokenPagedQueryHandler : IRequestHandler<SearchDepartmentsByTokenPagedQuery, DataSourceResult>
    {
        private readonly STSDbContext _context;
        public SearchDepartmentsByTokenPagedQueryHandler(STSDbContext context)
        {
            _context = context;
        }

        public async Task<DataSourceResult> Handle(SearchDepartmentsByTokenPagedQuery request, CancellationToken cancellationToken)
        {
            IQueryable<Department> query = _context.Departments.AsQueryable();
            query = query.Where(u => !u.Deleted);
            if (!string.IsNullOrWhiteSpace(request.Token))
            {
                query = query.Where(u => u.Code.Contains(request.Token)
                || u.Name.Contains(request.Token));
            }
            query = query.OrderByDescending(u => u.CreatedOn);

            PagedList<Department> queryResult = new PagedList<Department>();
            await queryResult.CreateAsync(query, request.DataSourceRequest.PageNum, request.DataSourceRequest.PageSize);
            List<SearchDepartmentsByTokenPagedDto> data = queryResult.Select(d => d.ToSearchDepartmentsByTokenPagedDto()).ToList();

            return new DataSourceResult
            {
                Data = data,
                Total = queryResult.TotalCount
            };
        }
    }
}
