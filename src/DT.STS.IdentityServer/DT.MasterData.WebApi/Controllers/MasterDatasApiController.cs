using DT.Core.Web.Common.Api.WebApi.Controllers;
using DT.STS.IdentityServer.Application.ActiveDirectories.Queries;
using DT.STS.IdentityServer.Application.Companies.Queries;
using DT.STS.IdentityServer.Application.Departments.Queries;
using DT.STS.IdentityServer.Application.Users.Queries;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace DT.MasterData.WebApi.Controllers
{
    //[Authorize]
    public class MasterDatasApiController : BaseApiController
    {
        public MasterDatasApiController()
        {
        }

        /// <summary>
        /// Get all departments
        /// </summary>
        /// <returns>List<GetAllDepartmentsDto></returns>
        [Route("api/v1/masterdatas/getalldepartments")]
        public async Task<List<GetAllDepartmentsDto>> GetAllDepartments()
        {
            return await Mediator.Send(new GetAllDepartmentsQuery());
        }

        /// <summary>
        /// Get all users
        /// </summary>
        /// <returns>List<GetAllUsersDto></returns>
        [Route("api/v1/masterdatas/getallusers")]
        [HttpGet]
        public async Task<List<GetAllUsersDto>> GetAllUsers()
        {
            return await Mediator.Send(new GetAllUsersQuery());
        }

        /// <summary>
        /// Get all companies
        /// </summary>
        /// <returns>List<GetAllCompaniesDto></returns>
        [Route("api/v1/masterdatas/getallcompanies")]
        [HttpGet]
        public async Task<List<GetAllCompaniesDto>> GetAllCompanies()
        {
            return await Mediator.Send(new GetAllCompaniesQuery());
        }

        /// <summary>
        /// Get all groups from active directory
        /// </summary>
        /// <returns>
        /// List<string>
        /// </returns>
        [Route("api/v1/masterdatas/getallgroupsfromactivedirectory")]
        [HttpGet]
        public async Task<List<string>> GetAllGroupsFromActiveDirectory()
        {
            return await Mediator.Send(new GetAllGroupsInActiveDirectoryQuery());
        }
    }
}
