using DT.STS.IdentityServer.Domain.Entities;
using DT.STS.IdentityServer.Persistence;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DT.STS.IdentityServer.Application.Departments.Commands
{
    public class UpdateDepartmentCommandHandler : IRequestHandler<UpdateDepartmentCommand, int>
    {
        private readonly STSDbContext _context;
        public UpdateDepartmentCommandHandler(STSDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(UpdateDepartmentCommand request, CancellationToken cancellationToken)
        {
            Department department = _context.Departments.FirstOrDefault(d => !d.Deleted && d.Id == request.Id);
            department.Code = request.Code;
            department.Name = request.Name;
            department.Email = request.Email;

            return await _context.SaveChangesAsync();
        }
    }
}
