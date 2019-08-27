using DT.STS.IdentityServer.Application.Mapper;
using DT.STS.IdentityServer.Persistence;
using MediatR;
using System.Data.Entity;
using System.Threading;
using System.Threading.Tasks;

namespace DT.STS.IdentityServer.Application.Departments.Queries
{
    public class GetDepartmentByIdQueryHandler : IRequestHandler<GetDepartmentByIdQuery, GetDepartmentByIdDto>
    {
        private readonly STSDbContext _context;
        public GetDepartmentByIdQueryHandler(STSDbContext context)
        {
            _context = context;
        }

        public async Task<GetDepartmentByIdDto> Handle(GetDepartmentByIdQuery request, CancellationToken cancellationToken)
        {
            GetDepartmentByIdDto getDepartmentByIdDto = new GetDepartmentByIdDto();
            Domain.Entities.Department department = await _context.Departments.FirstOrDefaultAsync(u => !u.Deleted && u.Id == request.Id);
            if (department != null)
            {
                getDepartmentByIdDto = department.ToGetDepartmentByIdDto();
            }
            return getDepartmentByIdDto;
        }
    }
}
