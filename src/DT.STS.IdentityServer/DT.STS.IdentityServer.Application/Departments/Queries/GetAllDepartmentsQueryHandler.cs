using DT.STS.IdentityServer.Application.Mapper;
using DT.STS.IdentityServer.Domain.Entities;
using DT.STS.IdentityServer.Persistence;
using MediatR;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DT.STS.IdentityServer.Application.Departments.Queries
{
    public class GetAllDepartmentsQueryHandler : IRequestHandler<GetAllDepartmentsQuery, List<GetAllDepartmentsDto>>
    {
        private readonly STSDbContext _context;

        public GetAllDepartmentsQueryHandler(STSDbContext context)
        {
            _context = context;
        }

        public async Task<List<GetAllDepartmentsDto>> Handle(GetAllDepartmentsQuery request, CancellationToken cancellationToken)
        {
            List<Department> departments = await _context.Departments.ToListAsync();

            List<GetAllDepartmentsDto> result = departments.Select(department => department.ToGetAllDepartmentsDto()).ToList();

            result.Add(new GetAllDepartmentsDto
            {
                Code = "Customer",
                Name = "Khách"
            });

            return result;
        }
    }
}
