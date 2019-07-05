using DT.STS.IdentityServer.Application.Departments.Queries;
using DT.STS.IdentityServer.Application.Users.Queries;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace DT.STS.IdentityServer.Mvc.Areas.Administration.Controllers.Apis
{
    [Authorize]
    public class MasterDatasApiController : BaseApiController
    {
        [Route("api/masterdatas/getalldepartments")]
        public async Task<List<GetAllDepartmentsDto>> GetAllDepartments()
        {
            return await Mediator.Send(new GetAllDepartmentsQuery());
        }

        [Route("api/masterdatas/getallusers")]
        [HttpGet]
        public async Task<List<GetAllUsersDto>> GetAllUsers()
        {
            return await Mediator.Send(new GetAllUsersQuery());
        }
    }
}
