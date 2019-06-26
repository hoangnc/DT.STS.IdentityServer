using DT.STS.IdentityServer.Application.Claims.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace DT.STS.IdentityServer.Mvc.Areas.Administration.Controllers.Apis
{
    public class ClaimsApiController : BaseApiController
    {
        [Route("api/claims/getallclaims")]
        [HttpGet]
        public async Task<List<GetAllClaimsDto>> GetAllClaims()
        {
            return await Mediator.Send(new GetAllClaimsQuery());
        }
    }
}
