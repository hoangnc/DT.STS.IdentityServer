using DT.STS.IdentityServer.Application.Mapper;
using DT.STS.IdentityServer.Domain.Entities;
using DT.STS.IdentityServer.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DT.STS.IdentityServer.Application.Companies.Queries
{
    public class GetAllCompaniesQueryHandler : IRequestHandler<GetAllCompaniesQuery, List<GetAllCompaniesDto>>
    {
        private readonly STSDbContext _context;
        public GetAllCompaniesQueryHandler(STSDbContext context)
        {
            _context = context;
        }

        public async Task<List<GetAllCompaniesDto>> Handle(GetAllCompaniesQuery request, CancellationToken cancellationToken)
        {
            List<Company> companies = await _context.Companies.ToListAsync();
            List<GetAllCompaniesDto> result = companies.Select(u => u.ToGetAllCompaniesDto()).ToList();
            return result;
        }
    }
}