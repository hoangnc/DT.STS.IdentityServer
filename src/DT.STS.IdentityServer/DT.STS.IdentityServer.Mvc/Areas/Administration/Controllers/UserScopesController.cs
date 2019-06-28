using DT.Core.Web.Ui.Navigation;
using DT.STS.IdentityServer.Mvc.Areas.Administration.Services;
using System.Web.Mvc;

namespace DT.STS.IdentityServer.Mvc.Areas.Administration.Controllers
{
    [Authorize]
    public class UserScopesController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        [Menu(SelectedMenu = MenuNameConstants.UserScope)]
        public ActionResult List()
        {
            return View();
        }
    }
}