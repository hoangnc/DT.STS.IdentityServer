using DT.STS.IdentityServer.Application.Users.Commands;
using DT.STS.IdentityServer.Application.Users.Queries;
using DT.STS.IdentityServer.Common.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace DT.STS.IdentityServer.Mvc.Areas.Administration.Controllers.Apis
{
    [Authorize]
    public class UsersApiController : BaseApiController
    {
        [Route("api/users/getuserspaged")]
        [HttpGet]
        public async Task<DataSourceResult> List([FromUri]DataSourceRequest dataSourceRequest)
        {
            return await Mediator.Send(new GetUsersPagedQuery {
                DataSourceRequest = dataSourceRequest
            });
        }

        [Route("api/users/syncusersfromad")]
        public async Task<int> SyncUsersFromAd()
        {
            return await Mediator.Send(new SyncUsersFromActiveDirectoryCommand {
                CreatedBy = User.Identity.Name,
                CreatedOn = DateTime.Now
            });
        }

        [Route("api/users/lookuserfromad")]
        [HttpGet]
        public async Task<GetUserByUserNameFromAdDto> LookUserFromAd(string userName)
        {
            return await Mediator.Send(new GetUserByUserNameFromAdQuery
            {
               UserName = userName
            });
        }

        [Route("api/users/searchusersbytokenpaged")]
        [HttpGet]
        public async Task<DataSourceResult> SearchUsersByTokenPaged([FromUri]DataSourceRequest dataSourceRequest,string token)
        {
            return await Mediator.Send(new SearchUsersByTokenPagedQuery
            {
                DataSourceRequest = dataSourceRequest,
                Token = token
            });
        }

        [Route("api/users/getallusers")]
        [HttpGet]
        public async Task<List<GetAllUsersDto>> GetAllUsers()
        {
            return await Mediator.Send(new GetAllUsersQuery());
        }
    }
}
