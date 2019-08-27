using DT.Core.Web.Common.Identity.Extensions;
using DT.STS.IdentityServer.Application.UserScopes.Commands;
using DT.STS.IdentityServer.Application.UserScopes.Queries;
using DT.STS.IdentityServer.Common.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace DT.STS.IdentityServer.Mvc.Areas.Administration.Controllers.Apis
{
    [Authorize]
    public class UserScopesApiController : BaseApiController
    {
        [Route("api/userscopes/searchuserscopesbytokenpaged")]
        [HttpGet]
        public async Task<DataSourceResult> SearchUserScopesByTokenPaged([FromUri]DataSourceRequest dataSourceRequest, string token)
        {
            DataSourceResult dataSourceResult = await Mediator.Send(new SearchUserScopesByTokenPagedQuery
            {
                DataSourceRequest = dataSourceRequest,
                Token = token
            });

            dataSourceResult.Data = dataSourceResult.Data.Cast<SearchUserScopesByTokenPagedDto>().ToList();

            return dataSourceResult;
        }

        [Route("api/userscopes/insertorupdate")]
        [HttpPost]
        public async Task<int> InsertOrUpdate([FromBody] InsertOrUpdateUserScopeCommand command)
        {
            command.CreatedBy = User.Identity.GetUserName();
            command.CreatedOn = DateTime.Now;
            return await Mediator.Send(command);
        }
    }
}
