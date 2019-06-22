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

namespace DT.STS.IdentityServer.Application.Clients.Queries
{
    public class SearchClientsByTokenPagedQueryHandler : IRequestHandler<SearchClientsByTokenPagedQuery, DataSourceResult>
    {
        private readonly STSDbContext _context;
        public SearchClientsByTokenPagedQueryHandler(STSDbContext context)
        {
            _context = context;
        }

        public async Task<DataSourceResult> Handle(SearchClientsByTokenPagedQuery request, CancellationToken cancellationToken)
        {
            IQueryable<Client> query = _context.Clients.AsQueryable();
            query = query.Where(u => !u.Deleted);

            if (!string.IsNullOrWhiteSpace(request.Token))
            {
                query = query.Where(u => u.ClientId.Contains(request.Token)
                || u.ClientName.Contains(request.Token)
                || u.ClientUri.Contains(request.Token));
            }
            query = query.OrderByDescending(u => u.CreatedOn);

            PagedList<Client> queryResult = new PagedList<Client>();
            await queryResult.CreateAsync(query, request.DataSourceRequest.PageNum, request.DataSourceRequest.PageSize);
            List<SearchClientsByTokenPagedDto> data = queryResult.Select(u => u.ToSearchClientsByTokenPagedDto()).ToList();

            return new DataSourceResult
            {
                Data = data,
                Total = queryResult.TotalCount
            };
        }
    }
}
