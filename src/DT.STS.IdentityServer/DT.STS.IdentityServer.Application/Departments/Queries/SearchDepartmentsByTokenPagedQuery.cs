using DT.STS.IdentityServer.Common.Models;
using MediatR;

namespace DT.STS.IdentityServer.Application.Departments.Queries
{
    public class SearchDepartmentsByTokenPagedQuery : IRequest<DataSourceResult>
    {
        public DataSourceRequest DataSourceRequest { get; set; }
        public string Token { get; set; }
    }
}
