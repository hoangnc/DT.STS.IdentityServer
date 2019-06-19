using DT.STS.IdentityServer.Application.Scopes.Commands;
using DT.STS.IdentityServer.Application.ScopeScopeClaims.Queries;
using DT.STS.IdentityServer.Mvc.Areas.Administration.Mapper;
using DT.STS.IdentityServer.Mvc.Areas.Administration.Models.Clients;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DT.STS.IdentityServer.Mvc.Areas.Administration.Controllers
{
    [Authorize]
    public class ClientsController : Controller
    {
        private readonly IMediator _mediator;

        public ClientsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: Administration/Clients
        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        public ActionResult List()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> Create()
        {
            ClientCreateModel model = new ClientCreateModel {
                RequireConsent = true,
                AllowRememberConsent = true,
                LogoutSessionRequired = true,
                AllowedCustomGrantTypes = true,
                EnableLocalLogin = true,
                PrefixClientClaims = true
            };

            var scopeClaims = await _mediator.Send(new GetAllScopeClaimsQuery());
            model.AvailableClaims = scopeClaims.Select(scopeClaim => new SelectListItem
            {
                Value = scopeClaim.Id.ToString(),
                Text = scopeClaim.Description
            }).ToList();

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Create(ClientCreateModel model)
        {
            if (ModelState.IsValid)
            {
                var createClientCommand = model.ToCreateClientCommand();
                createClientCommand.CreatedBy = User.Identity.Name;
                createClientCommand.CreatedOn = DateTime.Now;

                int result = await _mediator.Send(createClientCommand);
                if (result > 0)
                {
                    return View("List");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm Client thất bại");
                }
            }

            var scopeClaims = await _mediator.Send(new GetAllScopeClaimsQuery());
            model.AvailableClaims = scopeClaims.Select(scopeClaim => new SelectListItem
            {
                Value = scopeClaim.Id.ToString(),
                Text = scopeClaim.Description
            }).ToList();

            return View(model);
        }

    }
}