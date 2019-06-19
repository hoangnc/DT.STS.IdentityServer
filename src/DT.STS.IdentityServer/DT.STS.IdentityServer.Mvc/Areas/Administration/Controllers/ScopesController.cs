using DT.STS.IdentityServer.Application.Scopes.Queries;
using DT.STS.IdentityServer.Application.ScopeScopeClaims.Queries;
using DT.STS.IdentityServer.Mvc.Areas.Administration.Mapper;
using DT.STS.IdentityServer.Mvc.Areas.Administration.Models.Scopes;
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
    public class ScopesController : Controller
    {
        private readonly IMediator _mediator;

        public ScopesController(IMediator mediator)
        {
            _mediator = mediator;
        }
        // GET: Administration/Scope
        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        public ActionResult List()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> Update(int id)
        {
            ScopeUpdateModel model = new ScopeUpdateModel();
            var getScopeByIdDto = await _mediator.Send(new GetScopeByIdQuery {
                Id = id
            });

            if(getScopeByIdDto != null && getScopeByIdDto.Id > 0)
            {
                model = getScopeByIdDto.ToScopeUpdateModel();
                var scopeClaims = await _mediator.Send(new GetAllScopeClaimsQuery());
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

        [HttpPost]
        public async Task<ActionResult> Update(ScopeUpdateModel model)
        {
            if (ModelState.IsValid)
            {
                var updateScopeCommand = model.ToUpdateScopeCommand();
                updateScopeCommand.CreatedBy = User.Identity.Name;
                updateScopeCommand.CreatedOn = DateTime.Now;

                int result = await _mediator.Send(updateScopeCommand);
                if (result > 0)
                {
                    return View("List");
                }
                else
                {
                    ModelState.AddModelError("", "Update Scope thất bại");
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

        [HttpGet]
        public async Task<ActionResult> Create()
        {
            ScopeCreateModel model = new ScopeCreateModel();

            var scopeClaims = await _mediator.Send(new GetAllScopeClaimsQuery());
            model.AvailableClaims = scopeClaims.Select(scopeClaim => new SelectListItem
            {
                Value = scopeClaim.Id.ToString(),
                Text = scopeClaim.Description
            }).ToList();

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Create(ScopeCreateModel model)
        {
            if (ModelState.IsValid)
            {
                var createScopeCommand = model.ToCreateScopeCommand();
                createScopeCommand.CreatedBy = User.Identity.Name;
                createScopeCommand.CreatedOn = DateTime.Now;

                int result = await _mediator.Send(createScopeCommand);
                if (result > 0)
                {
                    return View("List");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm Scope thất bại");
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