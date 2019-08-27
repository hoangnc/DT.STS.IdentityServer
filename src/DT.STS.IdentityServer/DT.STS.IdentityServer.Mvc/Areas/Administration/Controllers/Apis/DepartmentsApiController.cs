using DT.Core.Web.Common.Identity.Extensions;
using DT.STS.IdentityServer.Application.Departments.Commands;
using DT.STS.IdentityServer.Application.Departments.Queries;
using DT.STS.IdentityServer.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace DT.STS.IdentityServer.Mvc.Areas.Administration.Controllers.Apis
{
    [Authorize]
    public class DepartmentsApiController : BaseApiController
    {
        [Route("api/departments/getalldepartments")]
        public async Task<List<GetAllDepartmentsDto>> GetAllDepartments()
        {
            return await Mediator.Send(new GetAllDepartmentsQuery());
        }

        [Route("api/departments/syncdepartmentsfromad")]
        public async Task<int> SyncUsersFromAd()
        {
            return await Mediator.Send(new SyncDepartmentsFromActiveDirectoryCommand
            {
                CreatedBy = User.Identity.GetUserName(),
                CreatedOn = DateTime.Now
            });
        }

        [Route("api/departments/searchdepartmentsbytokenpaged")]
        [HttpGet]
        public async Task<DataSourceResult> SearchUsersByTokenPaged([FromUri]DataSourceRequest dataSourceRequest, string token)
        {
            return await Mediator.Send(new SearchDepartmentsByTokenPagedQuery
            {
                DataSourceRequest = dataSourceRequest,
                Token = token
            });
        }
    }
}
