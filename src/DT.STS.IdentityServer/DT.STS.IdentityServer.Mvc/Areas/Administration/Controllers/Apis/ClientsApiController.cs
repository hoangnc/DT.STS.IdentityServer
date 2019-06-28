using DT.STS.IdentityServer.Application.Clients.Queries;
using DT.STS.IdentityServer.Common.Models;
using DT.STS.IdentityServer.Mvc.Areas.Administration.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Thinktecture.IdentityModel.Mvc;
using static DT.Core.Web.Common.Identity.Constants;

namespace DT.STS.IdentityServer.Mvc.Areas.Administration.Controllers.Apis
{
    [Authorize]
    public class ClientsApiController : BaseApiController
    {
        [Route("api/clients/searchclientsbytokenpaged")]
        [HttpGet]
        [ResourceAuthorize(DtPermissionBaseTypes.Read, IdentityServerResources.ApiClients)]
        [HandleForbidden]
        public async Task<DataSourceResult> SearchClientsByTokenPaged([FromUri]DataSourceRequest dataSourceRequest, string token)
        {
            var dataSourceResult = await Mediator.Send(new SearchClientsByTokenPagedQuery
            {
                DataSourceRequest = dataSourceRequest,
                Token = token
            });

            dataSourceResult.Data = dataSourceResult.Data.Cast<SearchClientsByTokenPagedDto>().Select(s => s.ToSearchClientViewModel()).ToList();

            return dataSourceResult;
        }
    }
}
