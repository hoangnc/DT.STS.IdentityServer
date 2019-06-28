using DT.STS.IdentityServer.Application.Departments.Queries;
using DT.STS.IdentityServer.Mvc.Areas.Administration.Models.Users;
using DT.STS.IdentityServer.Mvc.Areas.Administration.Mapper;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using System;
using IdentityServer3.Core.Models;
using DT.STS.IdentityServer.Application.Users.Queries;
using Thinktecture.IdentityModel.Mvc;
using static DT.Core.Web.Common.Identity.Constants;
using DT.Core.Web.Ui.Navigation;
using DT.STS.IdentityServer.Mvc.Areas.Administration.Services;

namespace DT.STS.IdentityServer.Mvc.Areas.Administration.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private readonly IMediator _mediator;
        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [ResourceAuthorize(DtPermissionBaseTypes.Read, IdentityServerResources.Users)]
        [HandleForbidden]
        public ActionResult Index()
        {

            return RedirectToAction("List");
        }

        [ResourceAuthorize(DtPermissionBaseTypes.Read, IdentityServerResources.Users)]
        [HandleForbidden]
        [Menu(SelectedMenu = MenuNameConstants.User)]
        public ActionResult List()
        {
            return View();
        }

        [ResourceAuthorize(DtPermissionBaseTypes.Write, IdentityServerResources.Users)]
        [HandleForbidden]
        [HttpGet]
        [Menu(SelectedMenu = MenuNameConstants.User)]
        public async Task<ActionResult> Create()
        {
            UserCreateModel model = new UserCreateModel();
            model.AvailableDomains = GetDomains();
            model.Departments = await GetDepartments();
            model.Users = await GetUsers();

            return View(model);
        }

        [ResourceAuthorize(DtPermissionBaseTypes.Write, IdentityServerResources.Users)]
        [HandleForbidden]
        [HttpPost]
        [Menu(SelectedMenu = MenuNameConstants.User)]
        public async Task<ActionResult> Create(UserCreateModel model)
        {
            if (ModelState.IsValid)
            {
                var createUserCommand = model.ToCreateUserCommand();
                createUserCommand.CreatedBy = User.Identity.Name;
                createUserCommand.CreatedOn = DateTime.Now;
                createUserCommand.Password = createUserCommand.Password.Sha256();
                int result = await _mediator.Send(createUserCommand);
                if (result > 0)
                {
                    return View("List");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm người dùng thất bại");
                }
            }

            model.AvailableDomains = GetDomains();
            model.Departments = await GetDepartments();

            return View(model);
        }

        private async Task<IList<SelectListItem>> GetDepartments()
        {
            List<GetAllDepartmentsDto> departments = await _mediator.Send(new GetAllDepartmentsQuery());

            return departments.Select(department => new SelectListItem
            {
                Text = department.Name,
                Value = department.Code
            }).ToList();
        }

        private async Task<IList<SelectListItem>> GetUsers()
        {
            List<GetAllUsersDto> users = await _mediator.Send(new GetAllUsersQuery());
            return users.Select(user => new SelectListItem {
                Text = $"{user.LastName} {user.FirstName};{user.DepartmentName}",
                Value = user.UserName
            }).ToList();
        }

        private IList<SelectListItem> GetDomains()
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