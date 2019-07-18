using DT.Core.Web.Ui.Navigation;
using DT.STS.IdentityServer.Mvc.Areas.Administration.Services;
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
    }
}