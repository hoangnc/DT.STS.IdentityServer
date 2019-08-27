using DT.Core.Web.Ui.Navigation;
using DT.STS.IdentityServer.Application.Departments.Commands;
using DT.STS.IdentityServer.Application.Departments.Queries;
using DT.STS.IdentityServer.Mvc.Areas.Administration.Services;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using Thinktecture.IdentityModel.Mvc;
using static DT.Core.Web.Common.Identity.Constants;

namespace DT.STS.IdentityServer.Mvc.Areas.Administration.Controllers
{
    [Authorize]
    public class DepartmentsController : IdentityServerControllerBase
    {
        // GET: Administration/Departments
        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        [Menu(SelectedMenu = MenuNameConstants.Department)]
        [ResourceAuthorize(DtPermissionBaseTypes.Write, IdentityServerResources.Clients)]
        public ActionResult List()
        {
            return View();
        }

        [Menu(SelectedMenu = MenuNameConstants.User)]
        [ResourceAuthorize(DtPermissionBaseTypes.Update, IdentityServerResources.Users)]
        [HandleForbidden]
        [HttpGet]
        public async Task<ActionResult> Update(int id)
        {
           
            GetDepartmentByIdDto departmentByIdDto = await Mediator.Send(new GetDepartmentByIdQuery
            {
                Id = id
            });

            return View(departmentByIdDto);
        }

        [Menu(SelectedMenu = MenuNameConstants.User)]
        [ResourceAuthorize(DtPermissionBaseTypes.Update, IdentityServerResources.Users)]
        [HandleForbidden]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<ActionResult> Update(GetDepartmentByIdDto dto)
        {
            if (ModelState.IsValid)
            {

                int result = await Mediator.Send(new UpdateDepartmentCommand {
                    Id = dto.Id,
                    Name = dto.Name,
                    Code = dto.Code,
                    Email = dto.Email
                });

                if (result > 0)
                {
                    return View("List");
                }
                else
                {
                    ModelState.AddModelError("", "Update User thất bại");
                }
            }

            return View(dto);
        }

    }
}