using DT.Core.Web.Ui.Navigation;
using DT.STS.IdentityServer.Application.Scopes.Queries;
using DT.STS.IdentityServer.Application.ScopeScopeClaims.Queries;
using DT.STS.IdentityServer.Mvc.Areas.Administration.Mapper;
using DT.STS.IdentityServer.Mvc.Areas.Administration.Models.Scopes;
using DT.STS.IdentityServer.Mvc.Areas.Administration.Services;
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
    public class ScopesController : IdentityServerControllerBase
    {
        // GET: Administration/Scope
        [ResourceAuthorize(DtPermissionBaseTypes.Read, IdentityServerResources.Scopes)]
        [HandleForbidden]
        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        [Menu(SelectedMenu = MenuNameConstants.Scope)]
        [ResourceAuthorize(DtPermissionBaseTypes.Read, IdentityServerResources.Scopes)]
        [HandleForbidden]
        public ActionResult List()
        {
            return View();
        }

        [Menu(SelectedMenu = MenuNameConstants.Scope)]
        [ResourceAuthorize(DtPermissionBaseTypes.Update, IdentityServerResources.Scopes)]
        [HandleForbidden]
        [HttpGet]
        public async Task<ActionResult> Update(int id)
        {
            ScopeUpdateModel model = new ScopeUpdateModel();
            GetScopeByIdDto getScopeByIdDto = await Mediator.Send(new GetScopeByIdQuery
            {
                Id = id
            });

            if (getScopeByIdDto != null && getScopeByIdDto.Id > 0)
            {
                model = getScopeByIdDto.ToScopeUpdateModel();
                List<GetAllScopeClaimsDto> scopeClaims = await Mediator.Send(new GetAllScopeClaimsQuery());
                model.AvailableClaims = scopeClaims.Select(scopeClaim => new SelectListItem
                {
                    Selected = true,
                    Value = scopeClaim.Id.ToString(),
                    Text = scopeClaim.Description
                }).ToList();

                return View(model);
            }

            return RedirectToAction("List");
        }

        [Menu(SelectedMenu = MenuNameConstants.Scope)]
        [ResourceAuthorize(DtPermissionBaseTypes.Update, IdentityServerResources.Scopes)]
        [HandleForbidden]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<ActionResult> Update(ScopeUpdateModel model)
        {
            if (ModelState.IsValid)
            {
                Application.Scopes.Commands.UpdateScopeCommand updateScopeCommand = model.ToUpdateScopeCommand();
                updateScopeCommand.CreatedBy = User.Identity.Name;
                updateScopeCommand.CreatedOn = DateTime.Now;

                int result = await Mediator.Send(updateScopeCommand);
                if (result > 0)
                {
                    return View("List");
                }
                else
                {
                    ModelState.AddModelError("", "Update Scope thất bại");
                }
            }

            List<GetAllScopeClaimsDto> scopeClaims = await Mediator.Send(new GetAllScopeClaimsQuery());
            model.AvailableClaims = scopeClaims.Select(scopeClaim => new SelectListItem
            {
                Value = scopeClaim.Id.ToString(),
                Text = scopeClaim.Description
            }).ToList();

            return View(model);
        }

        [Menu(SelectedMenu = MenuNameConstants.Scope)]
        [ResourceAuthorize(DtPermissionBaseTypes.Write, IdentityServerResources.Scopes)]
        [HandleForbidden]
        [HttpGet]
        public async Task<ActionResult> Create()
        {
            ScopeCreateModel model = new ScopeCreateModel();

            List<GetAllScopeClaimsDto> scopeClaims = await Mediator.Send(new GetAllScopeClaimsQuery());
            model.AvailableClaims = scopeClaims.Select(scopeClaim => new SelectListItem
            {
                Value = scopeClaim.Id.ToString(),
                Text = scopeClaim.Description
            }).ToList();

            return View(model);
        }

        [Menu(SelectedMenu = MenuNameConstants.Scope)]
        [ResourceAuthorize(DtPermissionBaseTypes.Write, IdentityServerResources.Scopes)]
        [HandleForbidden]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<ActionResult> Create(ScopeCreateModel model)
        {
            if (ModelState.IsValid)
            {
                Application.Scopes.Commands.CreateScopeCommand createScopeCommand = model.ToCreateScopeCommand();
                createScopeCommand.CreatedBy = User.Identity.Name;
                createScopeCommand.CreatedOn = DateTime.Now;

                int result = await Mediator.Send(createScopeCommand);
                if (result > 0)
                {
                    return View("List");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm Scope thất bại");
                }
            }

            List<GetAllScopeClaimsDto> scopeClaims = await Mediator.Send(new GetAllScopeClaimsQuery());
            model.AvailableClaims = scopeClaims.Select(scopeClaim => new SelectListItem
            {
                Value = scopeClaim.Id.ToString(),
                Text = scopeClaim.Description
            }).ToList();

            return View(model);
        }
    }
}