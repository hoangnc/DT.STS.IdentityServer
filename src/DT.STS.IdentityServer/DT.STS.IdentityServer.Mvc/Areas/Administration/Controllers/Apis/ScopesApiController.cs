using DT.STS.IdentityServer.Application.Scopes.Queries;
using DT.STS.IdentityServer.Common.Models;
using DT.STS.IdentityServer.Mvc.Areas.Administration.Mapper;
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
    public class ScopesApiController : BaseApiController
    {
        [Route("api/scopes/searchscopesbytokenpaged")]
        [HttpGet]
        public async Task<DataSourceResult> SearchUsersByTokenPaged([FromUri]DataSourceRequest dataSourceRequest, string token)
        {
            var dataSourceResult = await Mediator.Send(new SearchScopesByTokenPagedQuery
            {
                DataSourceRequest = dataSourceRequest,
                Token = token
            });

            dataSourceResult.Data = dataSourceResult.Data.Cast<SearchScopesByTokenPagedDto>().Select(s => s.ToSearchScopeViewModel()).ToList();

            return dataSourceResult;
        }
    }
}
