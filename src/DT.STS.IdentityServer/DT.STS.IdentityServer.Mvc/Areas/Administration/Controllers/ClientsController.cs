using DT.Core.Web.Ui.Navigation;
using DT.STS.IdentityServer.Application.Clients.Queries;
using DT.STS.IdentityServer.Application.Scopes.Queries;
using DT.STS.IdentityServer.Application.ScopeScopeClaims.Queries;
using DT.STS.IdentityServer.Mvc.Areas.Administration.Mapper;
using DT.STS.IdentityServer.Mvc.Areas.Administration.Models.Clients;
using DT.STS.IdentityServer.Mvc.Areas.Administration.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Thinktecture.IdentityModel.Mvc;
using static DT.Core.Web.Common.Identity.Constants;

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
        [ResourceAuthorize(DtPermissionBaseTypes.Read, IdentityServerResources.Clients)]
        [HandleForbidden]
        [HttpGet]
        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        [ResourceAuthorize(DtPermissionBaseTypes.Read, IdentityServerResources.Clients)]
        [HandleForbidden]
        [HttpGet]
        [Menu(SelectedMenu = MenuNameConstants.Client)]
        public ActionResult List()
        {
            return View();
        }

        [ResourceAuthorize(DtPermissionBaseTypes.Write, IdentityServerResources.Clients)]
        [HandleForbidden]
        [HttpGet]
        [Menu(SelectedMenu = MenuNameConstants.Client)]
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
            model.AvailableScopes = await GetScopes();

            return View(model);
        }

        [ResourceAuthorize(DtPermissionBaseTypes.Write, IdentityServerResources.Clients)]
        [HandleForbidden]
        [HttpPost]
        [Menu(SelectedMenu = MenuNameConstants.Client)]
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
            model.AvailableScopes = await GetScopes();
            model.InitData();

            return View(model);
        }

        [ResourceAuthorize(DtPermissionBaseTypes.Update, IdentityServerResources.Scopes)]
        [HandleForbidden]
        [HttpGet]
        [Menu(SelectedMenu = MenuNameConstants.Client)]
        public async Task<ActionResult> Update(string clientId)
        {
            ClientUpdateModel model = new ClientUpdateModel();
            var getClientByClientIdDto = await _mediator.Send(new GetClientByClientIdQuery
            {
                ClientId = clientId
            });

            if (getClientByClientIdDto != null && getClientByClientIdDto.Id > 0)
            {
                model = getClientByClientIdDto.ToClientUpdateModel();
                model.AvailableScopes = await GetScopes();
                model.AvailableClaims = await GetClaims();

                return View(model);
            }

            return RedirectToAction("List");
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

        private async Task<List<SelectListItem>> GetScopes()
        {
            List<GetScopesDto> scopes = await _mediator.Send(new GetScopesQuery());

            List<SelectListItem> selectListItems = scopes.Select(scope => new SelectListItem
            {
                Text = scope.DisplayName ?? scope.Name,
                Value = scope.Name
            }).ToList();

            return selectListItems;
        }

    }
}