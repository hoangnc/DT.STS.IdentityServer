using DT.STS.IdentityServer.Application.ScopeScopeClaims.Queries;
using DT.STS.IdentityServer.Mvc.Areas.Administration.Mapper;
using DT.STS.IdentityServer.Mvc.Areas.Administration.Models.Clients;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            ClientCreateModel model = new ClientCreateModel
            {
                RequireConsent = true,
                AllowRememberConsent = true,
                LogoutSessionRequired = true,
                AllowedCustomGrantTypes = true,
                EnableLocalLogin = true,
                PrefixClientClaims = true
            };

            model.AvailableClaims = await GetClaims();
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Create(ClientCreateModel model)
        {
            if (ModelState.IsValid)
            {
                Application.Clients.Commands.CreateClientCommand createClientCommand = model.ToCreateClientCommand();
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

            model.AvailableClaims = await GetClaims();
            model.InitData();
            return View(model);
        }

        private async Task<List<SelectListItem>> GetClaims()
        {
            List<GetAllScopeClaimsDto> scopeClaims = await _mediator.Send(new GetAllScopeClaimsQuery());
            List<SelectListItem> claims = scopeClaims.Select(scopeClaim => new SelectListItem
            {
                Value = scopeClaim.Id.ToString(),
                Text = scopeClaim.Description
            }).ToList();
            return claims;
        }

    }
}