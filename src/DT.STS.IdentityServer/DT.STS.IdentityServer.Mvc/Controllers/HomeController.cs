using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using Thinktecture.IdentityModel.Mvc;

namespace DT.STS.IdentityServer.Mvc.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            return RedirectToRoute("Dashboard");
        }

        [Authorize]
        public ActionResult About()
        {
            return View((User as ClaimsPrincipal).Claims);
        }

        [ResourceAuthorize("Read", "ContactDetails")]
        [HandleForbidden]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HandleForbidden]
        public ActionResult UpdateContact()
        {
            if (!HttpContext.CheckAccess("Write", "ContactDetails", "some more data"))
            {
                return this.AccessDenied();
            }

            ViewBag.Message = "Upate your contact details!";
            return View();
        }

        public ActionResult Logout()
        {
            Request.GetOwinContext().Authentication.SignOut();
            return Redirect("/");
        }
    }
}