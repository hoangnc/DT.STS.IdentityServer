using System.Web.Mvc;

namespace DT.STS.IdentityServer.Mvc.Areas.Administration.Controllers
{
    [Authorize]
    public class DepartmentsController : Controller
    {
        // GET: Administration/Departments
        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        public ActionResult List()
        {
            return View();
        }
    }
}