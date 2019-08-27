using DT.STS.IdentityServer.Domain.Entities;
using MediatR;

namespace DT.STS.IdentityServer.Application.Departments.Commands
{
    public class UpdateDepartmentCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
