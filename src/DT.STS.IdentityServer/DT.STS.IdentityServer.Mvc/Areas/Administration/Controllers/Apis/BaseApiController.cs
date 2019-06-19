using MediatR;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace DT.STS.IdentityServer.Mvc.Areas.Administration.Controllers.Apis
{
    public class BaseApiController : ApiController
    {
        private IMediator _mediator;

        protected IMediator Mediator => _mediator ?? (_mediator = Request.GetDependencyScope().GetService(typeof(IMediator)) as IMediator);

        protected IList<SelectListItem> GetDomains()
        {
            return new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = "Duy Tân",
                    Value = "duytan.local"
                },
                new SelectListItem
                {
                    Text = "Khách",
                    Value="Customer"
                }
            };
        }
    }
}