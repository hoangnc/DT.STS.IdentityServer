using MediatR;

namespace DT.STS.IdentityServer.Application.Departments.Queries
{
    public class GetDepartmentByIdQuery : IRequest<GetDepartmentByIdDto>
    { 
        public int Id { get; set; }
    }
}
