using DT.Core.Web.Ui.Navigation;
using DT.STS.IdentityServer.Mvc.Areas.Administration.Services;
using System.Web.Mvc;

namespace DT.STS.IdentityServer.Mvc.Areas.Administration.Controllers
{
    public class DashboardController : IdentityServerControllerBase
    {
        // GET: Administration/Dashboard
        [Authorize]
        [Menu(SelectedMenu = MenuNameConstants.User)]
        public ActionResult Index()
        {
            var user = User;
            return View();
        }
    }
}