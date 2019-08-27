using DT.Core.Web.Common.Identity.Extensions;
using DT.STS.IdentityServer.Application.Users.Commands;
using DT.STS.IdentityServer.Application.Users.Queries;
using DT.STS.IdentityServer.Common.Models;
using DT.STS.IdentityServer.Mvc.Helpers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Thinktecture.IdentityModel.WebApi;
using static DT.Core.Web.Common.Identity.Constants;

namespace DT.STS.IdentityServer.Mvc.Areas.Administration.Controllers.Apis
{
    [Authorize]
    public class UsersApiController : BaseApiController
    {
        private string PersonaPhotoUri => ConfigurationManager.AppSettings["PersonaPhotoUri"].ToString();

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
                CreatedBy = User.Identity.GetUserName(),
                CreatedOn = DateTime.Now
            });
        }

        [Route("api/users/syncimagesfromcloud")]
        [ResourceAuthorize(DtPermissionBaseTypes.Sync, IdentityServerResources.ApiUsers)]
        public async Task<int> SyncImagesFromCloud()
        {
            List<GetAllUsersDto> users = await Mediator.Send(new GetAllUsersQuery());
            IEnumerable<Task<dynamic>> userImages = users.Select(async user =>
            {
                byte[] image = await ImageLoaderHelper.GetImageToBytesFromUriAsync(new Uri(string.Format(PersonaPhotoUri, $"{user.UserName}@duytan.com")));

                dynamic userImage = new ExpandoObject();
                userImage.Id = user.Id;
                userImage.JpegPhoto = image;
                return userImage;
            });

            return await Mediator.Send(new SyncImagesFromCloudCommand
            {
                Users = userImages.Select(user => user.Result).ToList()
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
