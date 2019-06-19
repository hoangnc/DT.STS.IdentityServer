using MediatR;
using System.Collections.Generic;

namespace DT.STS.IdentityServer.Application.Departments.Queries
{
    public class GetAllDepartmentsQuery : IRequest<List<GetAllDepartmentsDto>>
    {
    }
}
