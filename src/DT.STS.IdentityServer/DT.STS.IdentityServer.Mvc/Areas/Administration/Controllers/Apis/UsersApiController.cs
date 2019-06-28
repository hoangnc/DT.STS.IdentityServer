using DT.STS.IdentityServer.Application.Users.Commands;
using DT.STS.IdentityServer.Application.Users.Queries;
using DT.STS.IdentityServer.Common.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using Thinktecture.IdentityModel.WebApi;
using static DT.Core.Web.Common.Identity.Constants;

namespace DT.STS.IdentityServer.Mvc.Areas.Administration.Controllers.Apis
{
    [Authorize]
    public class UsersApiController : BaseApiController
    {
        [Route("api/users/getuserspaged")]
        [HttpGet]
        [ResourceAuthorize(DtPermissionBaseTypes.Read, IdentityServerResources.ApiUsers)]
        public async Task<DataSourceResult> List([FromUri]DataSourceRequest dataSourceRequest)
        {
            return await Mediator.Send(new GetUsersPagedQuery
            {
                DataSourceRequest = dataSourceRequest
            });
        }

        [Route("api/users/syncusersfromad")]
        [ResourceAuthorize(DtPermissionBaseTypes.Sync, IdentityServerResources.ApiUsers)]
        public async Task<int> SyncUsersFromAd()
        {
            return await Mediator.Send(new SyncUsersFromActiveDirectoryCommand
            {
                CreatedBy = User.Identity.Name,
                CreatedOn = DateTime.Now
            });
        }

        [Route("api/users/lookuserfromad")]
        [HttpGet]
        [ResourceAuthorize(DtPermissionBaseTypes.Read, IdentityServerResources.ApiUsers)]
        public async Task<GetUserByUserNameFromAdDto> LookUserFromAd(string userName)
        {
            return await Mediator.Send(new GetUserByUserNameFromAdQuery
            {
                UserName = userName
            });
        }

        [Route("api/users/searchusersbytokenpaged")]
        [HttpGet]
        [ResourceAuthorize(DtPermissionBaseTypes.Read, IdentityServerResources.ApiUsers)]
        public async Task<DataSourceResult> SearchUsersByTokenPaged([FromUri]DataSourceRequest dataSourceRequest, string token)
        {
            return await Mediator.Send(new SearchUsersByTokenPagedQuery
            {
                DataSourceRequest = dataSourceRequest,
                Token = token
            });
        }

        [Route("api/users/getallusers")]
        [HttpGet]
        [ResourceAuthorize(DtPermissionBaseTypes.Read, IdentityServerResources.ApiUsers)]
        public async Task<List<GetAllUsersDto>> GetAllUsers()
        {
            return await Mediator.Send(new GetAllUsersQuery());
        }
    }
}
